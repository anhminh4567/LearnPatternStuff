using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;

using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace TestConceptPattern.Services
{
    public class AzureBlobServices
    {
        private string containerName = "music";
        private string storageAccount = "testingaudio";
        private string sasToken = "sp=r&st=2024-03-05T08:14:34Z&se=2024-04-02T16:14:34Z&sv=2022-11-02&sr=c&sig=J6ODVfLPmzL3qavMGQAleuBZc0a9eZPeNmSVDzwwZMc%3D";
        private string sasUrl = "https://testingaudio.blob.core.windows.net/music?sp=r&st=2024-03-05T08:14:34Z&se=2024-04-02T16:14:34Z&sv=2022-11-02&sr=c&sig=J6ODVfLPmzL3qavMGQAleuBZc0a9eZPeNmSVDzwwZMc%3D";
        private string key = "/rGQd4CMRYkRhcQzqiYaTSp83u5OqP21mh+b3/7JN8dX11hcxU5UA65SuLxqStK0yHvbNhrBeEGX+AStwp99FA==";
        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=testingaudio;AccountKey=/rGQd4CMRYkRhcQzqiYaTSp83u5OqP21mh+b3/7JN8dX11hcxU5UA65SuLxqStK0yHvbNhrBeEGX+AStwp99FA==;EndpointSuffix=core.windows.net";
        private readonly BlobContainerClient _blobContainerClient;
        private string _blobUri = "https://testingaudio.blob.core.windows.net";
        public AzureBlobServices()
        {
            var credential = new StorageSharedKeyCredential(storageAccount, key);
            var blobUri = _blobUri;
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri),credential) ;
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }
        public void StreamFile()
        {

        }
        public async Task<(Stream stream, string contentType)?> DownloadFile(string filename)
        {
            var file = _blobContainerClient.GetBlobClient(filename);
            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                var content = await file.DownloadContentAsync();
                var contentType = content.Value.Details.ContentType;
                return (stream : data, contentType : contentType );
            }
            return null;
        }
        public async Task StreamAudio(string filename,HttpContext httpContext)
        {
            var blobClient = _blobContainerClient.GetBlobClient(filename);
            var request = httpContext.Request;
            var response = httpContext.Response;
            
            if (await blobClient.ExistsAsync())
            {
                var blobProperties = blobClient.GetProperties().Value;
                response.ContentType = "audio/mpeg";
                response.Headers.ContentLength = blobProperties.ContentLength;
                //chat gpt generated
                //if (request.Headers.ContainsKey("Range"))
                //{
                //    response.StatusCode = (int)HttpStatusCode.PartialContent;

                //    var range = RangeHeaderValue.Parse(request.Headers["Range"]).Ranges.First();
                //    var start = range.From ?? 0;
                //    var end = range.To ?? blobProperties.ContentLength - 1;
                //    var contentLength = end - start + 1;

                //    response.Headers.ContentRange = new ContentRangeHeaderValue(start, end, blobProperties.ContentLength).ToString();
                //    response.Headers.ContentLength = contentLength;
                //    var downloadOption = new BlobDownloadToOptions();
                //    await blobClient.DownloadToAsync(response.Body, options: );//, new BlobDownloadToOptions() {  } new Azure.HttpRange(start, contentLength)
                //}
                //else
                //{
                    await blobClient.DownloadToAsync(response.Body);
                //}

            }
//return null;
        }
        public async Task DeleteFile(string blobFileName)
        {
            var file = _blobContainerClient.GetBlobClient(blobFileName);
            await file.DeleteAsync();
        }
        public async Task UploadFile(IFormFile file)
        {
            var client = _blobContainerClient.GetBlobClient(file.FileName);
            using(Stream s = file.OpenReadStream())
            {
                await client.UploadAsync(s);
            }
            var uri = client.Uri.AbsoluteUri;
            var name = client.Name;
        }
        public List<string> ListAsync()
        {
            var returnList = new List<string>();
            var getBlobs = _blobContainerClient.GetBlobs();
            var isBlobExist = _blobContainerClient.Exists();
            foreach(var blob in getBlobs)
            {
                string uri = _blobContainerClient.Uri.ToString();
                var name = blob.Name;
                var fullUri = $"{uri}/{name}";
                returnList.Add(fullUri);   
            }
            return returnList;
        }
    }
}
