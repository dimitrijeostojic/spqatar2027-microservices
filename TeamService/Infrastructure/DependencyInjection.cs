using Application.EventConsumers;
using Domain.Abstractions;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.RepositoryImplementations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    const string _connectionName = "DefaultConnection";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(_connectionName)));

        services.AddRepositories();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumer<MatchCompletedEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                cfg.Host(options.Host, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.TryAddScoped<ITeamRepository, TeamRepository>();
        services.TryAddScoped<IGroupRepository, GroupRepository>();
        services.TryAddScoped<ITeamStandingRepository, TeamStandingRepository>();
    }
}
