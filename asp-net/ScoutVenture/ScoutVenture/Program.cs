using AppSettings;
using ScoutVenture.Core;
using ScoutVenture.Extensions;
using ScoutVenture.PostgresAdapter;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Load configurations
            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.SmtpOptionsKey));
            builder.Services.Configure<HostInformation>(
                builder.Configuration.GetSection(HostInformation.HostInformationKey));

            builder.Services.AddLogging(b => b.AddConsole());

            // Add services to the container.
            builder.Services.AddIdentity();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentCors", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:4200",   // Angular dev server default
                        "https://localhost:4200",  // Angular dev server with HTTPS
                        "http://localhost:80",     // Traefik proxy
                        "https://localhost:80"     // Traefik proxy with HTTPS
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });

                options.AddPolicy("ProductionCors", policy =>
                {
                    var hostInfo = builder.Configuration.GetSection(HostInformation.HostInformationKey).Get<HostInformation>();
                    var baseUrl = hostInfo?.BaseUrl ?? "https://localhost";
                    
                    policy.WithOrigins(baseUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // Add rate limiting
            builder.Services.AddRateLimiter(options =>
            {
                // Global rate limiting - 100 requests per minute per IP
                options.GlobalLimiter = System.Threading.RateLimiting.PartitionedRateLimiter.Create<HttpContext, string>(
                    httpContext => System.Threading.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: partition => new System.Threading.RateLimiting.FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1)
                        }));

                // Authentication endpoints - stricter limits (5 attempts per minute per IP)
                options.AddPolicy("AuthPolicy", httpContext =>
                    System.Threading.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: partition => new System.Threading.RateLimiting.FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1)
                        }));

                // Admin endpoints - moderate limits (20 requests per minute per IP)
                options.AddPolicy("AdminPolicy", httpContext =>
                    System.Threading.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: partition => new System.Threading.RateLimiting.FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 20,
                            Window = TimeSpan.FromMinutes(1)
                        }));

                // Rejection response
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken: token);
                };
            });

            builder.AddPostgres();

            builder.AddServices();

            // Error Handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();


            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ApplyPostgresMigrations();
            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UsePathBase("/api");

            // Use CORS - environment-specific policy
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("DevelopmentCors");
            }
            else
            {
                app.UseCors("ProductionCors");
            }

            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapIdentityApi<UserDpo>().RequireRateLimiting("AuthPolicy");

            app.Run();
        }
    }
}