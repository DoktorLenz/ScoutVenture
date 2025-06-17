using AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.Core;
using ScoutVenture.Extensions;
using ScoutVenture.PostgresAdapter;
using SmtpAdapter;

namespace ScoutVenture
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configurations
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.SmtpOptionsKey));
            builder.Services.Configure<HostInformation>(builder.Configuration.GetSection(HostInformation.HostInformationKey));
            
            // Add services to the container.
            builder.Services.AddIdentity();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.AddPostgres();
            
            builder.AddServices();
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.ApplyPostgresMigrations();
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UsePathBase("/api");
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapIdentityApi<IdentityUser>();
            
            app.Run();
        }
        
        
    }
}

