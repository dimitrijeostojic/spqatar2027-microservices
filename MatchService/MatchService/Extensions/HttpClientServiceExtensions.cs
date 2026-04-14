using Application.Contracts;
using Infrastructure.HttpClients;
using Infrastructure.Options;
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MatchService.Extensions;

public static class HttpClientServiceExtensions
{
    public static void AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<ITeamServiceClient, TeamServiceClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<ServiceUrlsOptions>>().Value;
            client.BaseAddress = new Uri(options.TeamService);
        });

        services.AddHttpClient<IStadiumServiceClient, StadiumServiceClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<ServiceUrlsOptions>>().Value;
            client.BaseAddress = new Uri(options.StadiumService);
        });
    }
}
