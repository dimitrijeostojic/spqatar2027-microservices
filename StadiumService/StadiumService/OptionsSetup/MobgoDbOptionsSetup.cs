using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace StadiumService.OptionsSetup;

public sealed class MobgoDbOptionsSetup(IConfiguration configuration) : IConfigureOptions<MongoDbOptions>
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private const string SectionName = "MongoDb";

    public void Configure(MongoDbOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
