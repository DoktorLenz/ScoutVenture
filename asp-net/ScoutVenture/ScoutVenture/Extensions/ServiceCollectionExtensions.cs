﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.CoreContracts;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.PostgresAdapter;
using SmtpAdapter;

namespace ScoutVenture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
            services.AddIdentityCore<IdentityUser>(options =>
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
                }).AddEntityFrameworkStores<PostgresApplicationDbContext>()
                .AddApiEndpoints();
            services.AddTransient<IEmailSender<IdentityUser>, IdentityMailSender>();
            services.AddAuthorization();
            return services;
        }
    }
}
