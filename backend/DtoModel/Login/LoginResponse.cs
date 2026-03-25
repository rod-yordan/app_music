namespace DtoModel.Login;

public class LoginResponse
{
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}
