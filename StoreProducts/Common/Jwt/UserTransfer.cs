namespace Common.Jwt;

public class UserTransfer
{
    public string Email { get; set; }
    public string IdentityUser { get; set; }

    public UserTransfer(string email,string identityUser)
    {
        Email=email;
        IdentityUser=identityUser;
    }
}