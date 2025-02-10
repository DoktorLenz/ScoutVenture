using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.ApiControllers;
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
            
            // Add services to the container.
            // builder.Services.AddKeycloakAuthentication(builder.Configuration);
            builder.Services.AddIdentity();

            builder.Services.AddControllers().AddApplicationPart(typeof(WeatherForecastController).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<PostgresApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
                    o => o.SetPostgresVersion(17, 2))
                    .UseSeeding(Seeding.Seed).UseAsyncSeeding(SeedingAsync.SeedAsync)
            );

            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
                app.ApplyMigrations();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapIdentityApi<IdentityUser>();

            app.Run();
        }
    }
}

