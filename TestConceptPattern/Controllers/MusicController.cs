using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TestConceptPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        public MusicController()
        {
        }
        //public async IAsyncEnumerable StreamMusic()
        //{
        //    int count = 0;
        //    while (true)
        //    {
        //        if (count >= 5)
        //            break;
        //        count++;
        //        await Task.Delay(500);
        //        yield return "content is : " + count + "\n";
        //    }
        //}
        //[HttpGet]
        //public async Task<HttpResponseMessage> StreamMusic()
        //{
        //    var response = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream);
        //    return await Response.Body.WriteAsync(response.s);
        //}
        //public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        //{
        //    //path of file which we have to read//  
        //    var filePath = "C:\\Users\\anhmi\\Downloads\\HongKong1[DEMO] - Nguyễn Trọng Tài.wav";
        //    //here set the size of buffer, you can set any size  
        //    int bufferSize = 1024;
        //    byte[] buffer = new byte[bufferSize];
        //    //here we re using FileStream to read file from server//  
        //    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        int totalSize = (int)fileStream.Length;
        //        /*here we are saying read bytes from file as long as total size of file 
        //        is greater then 0*/
        //        while (totalSize > 0)
        //        {
        //            int count = totalSize > bufferSize
        //                ? bufferSize
        //                : totalSize;
        //            //here we are reading the buffer from orginal file  
        //            int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
        //            //here we are writing the readed buffer to output//  
        //            await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
        //            //and finally after writing to output stream decrementing it to total size of file.  
        //            totalSize -= sizeOfReadedBuffer;
        //        }
        //    }

        //}
    }
    
}
