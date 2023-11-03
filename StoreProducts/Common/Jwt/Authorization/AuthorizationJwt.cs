using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Jwt.Authorization;

public class AuthorizationJwt : IAuthorizationJwt
{
    private readonly IConfiguration _configuration;

    public AuthorizationJwt(IConfiguration configuration) => _configuration = configuration;
    private JwtConfig GetJwtConfigByAppSetting() => _configuration.GetSection("JwtConfig").Get<JwtConfig>();

    public string CreateToken(IEnumerable<string> roles, string userName)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(roles, userName);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(GetJwtConfigByAppSetting().Secret);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(IEnumerable<string> roles, string userName)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, userName) };
        if (roles is not null)
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        return new JwtSecurityToken
        (
            issuer: GetJwtConfigByAppSetting().ValidIssuer,
            audience: GetJwtConfigByAppSetting().ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(GetJwtConfigByAppSetting().ExpiresIn)),
            signingCredentials: signingCredentials
        );
    }
}