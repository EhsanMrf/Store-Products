namespace Common.Jwt;

public class JwtConfig
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string Secret { get; set; }
    public string ExpiresIn { get; set; }
}