namespace Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SigningKey { get; set; } = string.Empty;
    public int ExpirationSeconds { get; set; }
    public int Validity { get; set; }
}