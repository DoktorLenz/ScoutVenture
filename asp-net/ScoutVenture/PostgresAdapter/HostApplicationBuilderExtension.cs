using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoutVenture.CoreContracts;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.PostgresAdapter
{
    public static class HostApplicationBuilderExtension
    {
        public static IHostApplicationBuilder AddPostgres(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<PostgresApplicationDbContext>(options => options
                .UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
                    o => o
                        .SetPostgresVersion(17, 2)
                        .MapEnum<Gender>()
                        .MapEnum<Rank>()
                )
                .UseExceptionProcessor()
            );
            builder.Services.AddScoped<ITransactionManager, TransactionManager>();
            builder.Services.AddTransient<IMemberRepository, MemberRepository>();
            return builder;
        }

        public static void ApplyPostgresMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using PostgresApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<PostgresApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}