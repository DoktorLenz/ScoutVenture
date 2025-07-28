using Microsoft.AspNetCore.Identity;
using ScoutVenture.Constants;
using ScoutVenture.PostgresAdapter;
using ScoutVenture.PostgresAdapter.Entities;
using SmtpAdapter;

namespace ScoutVenture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<UserDpo, IdentityRole>(options =>
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
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Admin));
                options.AddPolicy("CounselorOrAbove", policy => policy.RequireRole(Roles.Admin, Roles.Counselor));
                options.AddPolicy("MemberOrAbove", policy => policy.RequireRole(Roles.Admin, Roles.Counselor, Roles.Member));
            });
            
            return services;
        }
    }
}