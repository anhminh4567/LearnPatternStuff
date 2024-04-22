using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;
using TestConceptPattern;
using TestConceptPattern.Caching;
using TestConceptPattern.Databases;
using TestConceptPattern.Databases.Model;
using TestConceptPattern.Dto;
using TestConceptPattern.Repositories.Implementation;
using TestConceptPattern.Repositories.Implementation.Transac;
using TestConceptPattern.Repositories.Interfaces;
using TestConceptPattern.Repositories.Interfaces.Transac;
using TestConceptPattern.Services;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorPages();
		builder.Services.AddDbContext<SchoolDbContext>(contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped);
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
        builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
		builder.Services.AddSingleton<AzureBlobServices>();
		builder.Services.AddSingleton<IConnectionMultiplexer>(factory => ConnectionMultiplexer.Connect("localhost:6379"));
		builder.Services.AddSingleton<ICacheService,RedisCachesService>();
		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
		});
		builder.Services.AddAutoMapper(config =>
		{
			config.CreateMap<Student,StudentDto>();
		},typeof(Program).Assembly);
		builder.Services.AddSignalR();
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
			app.Use( async (context, next) => 
			{
				using IServiceScope scope = context.RequestServices.CreateScope();
				using SchoolDbContext dbContext = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

				dbContext.Database.Migrate();
                await next();
			} );
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapRazorPages();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapRazorPages();
			endpoints.MapControllers();
			endpoints.MapHub<SignalRHub>("/SignalR");
		});
		app.Run();
	}
}