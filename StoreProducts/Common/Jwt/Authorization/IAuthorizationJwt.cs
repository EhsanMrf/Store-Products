namespace Common.Jwt.Authorization;

public interface IAuthorizationJwt
{
    string CreateToken(IEnumerable<string> roles, UserTransfer userTransfer);
}