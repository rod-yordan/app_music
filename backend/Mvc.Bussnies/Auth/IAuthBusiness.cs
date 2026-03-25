using DtoModel.Login;

namespace Mvc.Business.Auth;

public interface IAuthBusiness : IDisposable
{
    Task<LoginResponse?> Login(LoginRequest request);
    Task<bool> IsCredentialsOk(LoginRequest request);
}
