using Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace api.OptionSetup;

public class JwtOptionSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public const string SectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}