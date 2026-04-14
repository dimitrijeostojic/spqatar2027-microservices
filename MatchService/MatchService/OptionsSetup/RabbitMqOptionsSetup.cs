using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace MatchService.OptionsSetup;

public sealed class RabbitMqOptionsSetup(IConfiguration configuration) : IConfigureOptions<RabbitMqOptions>
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private readonly string SectionName = "RabbitMq";

    public void Configure(RabbitMqOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
