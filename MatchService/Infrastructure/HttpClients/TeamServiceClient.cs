using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HttpClients;

public sealed class TeamServiceClient(HttpClient httpClient) : ITeamServiceClient
{
    public async Task<TeamValidationResponse?> GetTeamByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<TeamValidationResponse>($"api/team/{publicId}/exists", cancellationToken);
        }
        catch
        {
            return null;
        }
    }
}