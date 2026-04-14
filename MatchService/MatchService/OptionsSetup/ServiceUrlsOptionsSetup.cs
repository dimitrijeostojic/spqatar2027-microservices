using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace MatchService.OptionsSetup;

public sealed class ServiceUrlsOptionsSetup(IConfiguration configuration) : IConfigureOptions<ServiceUrlsOptions>
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private readonly string SectionName = "ServiceUrls";

    public void Configure(ServiceUrlsOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
