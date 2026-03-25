using Dapper;
using DtoModel.Login;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mvc.Business.Auth;

public class AuthBusiness : IAuthBusiness
{
    private readonly IDbConnection _db;

    public AuthBusiness(IDbConnection db)
    {
        _db = db;
    }

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<bool> IsCredentialsOk(LoginRequest request)
    {
        var query = @"SELECT password 
                      FROM usuario 
                      WHERE username = @Username";

        var user = await _db.QueryFirstOrDefaultAsync(query, new
        {
            request.Username
        });

        if (user == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(request.Password, (string)user.password);
    }

    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        var query = @"SELECT id, username, password 
                      FROM usuario 
                      WHERE username = @Username";

        var user = await _db.QueryFirstOrDefaultAsync(query, new
        {
            request.Username
        });

        if (user == null)
            return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, (string)user.password);

        if (!isValid)
            return null;

        var key = "ESTA_ES_UNA_CLAVE_SUPER_SECRETA_12345";

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim("UserId", user.id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginResponse
        {
            UserId = user.id.ToString(),
            Token = tokenHandler.WriteToken(token),
            Expiration = tokenDescriptor.Expires.Value
        };
    }
}