using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace api.OptionSetup;

public class CorsOptionSetup : IConfigureNamedOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    }

    public void Configure(string? name, CorsOptions options)
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    }
}