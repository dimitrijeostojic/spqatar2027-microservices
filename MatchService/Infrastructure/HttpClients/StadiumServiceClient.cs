using Application.Contracts;
using System.Net.Http.Json;

namespace Infrastructure.HttpClients;

public sealed class StadiumServiceClient(HttpClient httpClient) : IStadiumServiceClient
{
    public async Task<StadiumValidationResponse?> GetStadiumByPublicIdAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<StadiumValidationResponse>($"api/stadium/{publicId}/exists", cancellationToken);
        }
        catch
        {
            return null;
        }
    }
}