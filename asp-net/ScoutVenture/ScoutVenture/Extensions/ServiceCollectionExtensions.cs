using Microsoft.AspNetCore.Identity;
using ScoutVenture.PostgresAdapter;
using ScoutVenture.PostgresAdapter.Entities;
using SmtpAdapter;

namespace ScoutVenture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
            services.AddIdentityCore<UserDpo>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireNonAlphanumeric = false,
                        RequireUppercase = false,
                        RequiredUniqueChars = 0,
                        RequiredLength = 8
                    };
                })
                .AddEntityFrameworkStores<PostgresApplicationDbContext>()
                .AddApiEndpoints();
            services.AddTransient<IEmailSender<UserDpo>, IdentityMailSender>();
            services.AddAuthorization();
            return services;
        }
    }
}