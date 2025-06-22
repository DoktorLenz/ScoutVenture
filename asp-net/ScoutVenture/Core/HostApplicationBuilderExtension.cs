using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.Core
{
    public static class HostApplicationBuilderExtension
    {
        public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddTransient<IMemberService, MemberService>();
            builder.Services.AddTransient<IUserService, UserService>();
            return builder;
        }
    }
}