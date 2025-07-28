using Microsoft.Extensions.Options;

namespace ScoutVenture.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecurityHeadersOptions _options;

        public SecurityHeadersMiddleware(RequestDelegate next, IOptions<SecurityHeadersOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // X-Content-Type-Options: Prevent MIME-type sniffing
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

            // X-Frame-Options: Prevent clickjacking
            context.Response.Headers.Append("X-Frame-Options", "DENY");

            // X-XSS-Protection: Enable XSS filtering (legacy browsers)
            context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");

            // Referrer-Policy: Control referrer information
            context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

            // Permissions-Policy: Control browser features
            context.Response.Headers.Append("Permissions-Policy", 
                "geolocation=(), microphone=(), camera=(), payment=(), usb=(), magnetometer=(), gyroscope=()");

            // Content-Security-Policy: Prevent XSS and data injection attacks
            var csp = BuildContentSecurityPolicy(context);
            context.Response.Headers.Append("Content-Security-Policy", csp);

            // Strict-Transport-Security: Enforce HTTPS (only in production with HTTPS)
            if (context.Request.IsHttps && !_options.IsDevelopment)
            {
                context.Response.Headers.Append("Strict-Transport-Security", 
                    "max-age=31536000; includeSubDomains; preload");
            }

            // Cross-Origin-Embedder-Policy: Prevent cross-origin attacks
            context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "require-corp");

            // Cross-Origin-Opener-Policy: Prevent cross-origin attacks
            context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");

            // Cross-Origin-Resource-Policy: Control cross-origin resource sharing
            context.Response.Headers.Append("Cross-Origin-Resource-Policy", "cross-origin");

            await _next(context);
        }

        private string BuildContentSecurityPolicy(HttpContext context)
        {
            var baseUrl = _options.BaseUrl ?? "https://localhost";
            
            // Build CSP based on environment
            if (_options.IsDevelopment)
            {
                // More permissive CSP for development
                return "default-src 'self' 'unsafe-inline' 'unsafe-eval' http://localhost:* https://localhost:*; " +
                       "img-src 'self' data: blob: http://localhost:* https://localhost:*; " +
                       "connect-src 'self' http://localhost:* https://localhost:* ws://localhost:* wss://localhost:*; " +
                       "font-src 'self' data: https://fonts.gstatic.com; " +
                       "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
                       "frame-ancestors 'none'; " +
                       "base-uri 'self'; " +
                       "form-action 'self';";
            }
            else
            {
                // Strict CSP for production
                return $"default-src 'self'; " +
                       $"script-src 'self' 'nonce-{GenerateNonce()}'; " +
                       $"style-src 'self' 'nonce-{GenerateNonce()}' https://fonts.googleapis.com; " +
                       $"img-src 'self' data: blob:; " +
                       $"connect-src 'self' {baseUrl}; " +
                       $"font-src 'self' https://fonts.gstatic.com; " +
                       $"frame-ancestors 'none'; " +
                       $"base-uri 'self'; " +
                       $"form-action 'self'; " +
                       $"upgrade-insecure-requests;";
            }
        }

        private static string GenerateNonce()
        {
            var bytes = new byte[16];
            Random.Shared.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }

    public class SecurityHeadersOptions
    {
        public bool IsDevelopment { get; set; }
        public string? BaseUrl { get; set; }
    }

    public static class SecurityHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityHeadersMiddleware>();
        }

        public static IServiceCollection AddSecurityHeaders(this IServiceCollection services, 
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.Configure<SecurityHeadersOptions>(options =>
            {
                options.IsDevelopment = environment.IsDevelopment();
                var hostInfo = configuration.GetSection("HostInformation").Get<AppSettings.HostInformation>();
                options.BaseUrl = hostInfo?.BaseUrl;
            });

            return services;
        }
    }
}