using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.Core
{
    public static class HostApplicationBuilderExtension
    {
        public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddTransient<IMemberService, MemberService>();
            return builder;
        }
    }
}