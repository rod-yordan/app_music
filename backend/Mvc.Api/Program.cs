using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Mvc.Business.Auth;
using Mvc.Bussnies.Cancion;
using Mvc.Bussnies.GeneroCancion;
using Mvc.Bussnies.Persona;
using Mvc.Bussnies.PersonaTipoDocumentoB;

using Mvc.Repository.CancionRepo.Contratos;
using Mvc.Repository.CancionRepo.Implementacion;
using Mvc.Repository.GeneroCancionRepo.Contratos;
using Mvc.Repository.GeneroCancionRepo.Implementacion;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepo.Implementacion;
using Mvc.Repository.PersonaTipoDocumentoRepo.Contratos;
using Mvc.Repository.PersonaTipoDocumentoRepo.Implementacion;

using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var key = "ESTA_ES_UNA_CLAVE_SUPER_SECRETA_12345";

builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    return new MySqlConnection(connectionString);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});

builder.Services.AddScoped<IAuthBusiness, AuthBusiness>();

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();

builder.Services.AddScoped<IPersonaTipoDocumentoRepository, PersonaTipoDocumentoRepository>();
builder.Services.AddScoped<IPersonaTipoDocumentoBussnies, PersonaTipoDocumentoBussnies>();

builder.Services.AddScoped<ICancionRepository, CancionRepository>();
builder.Services.AddScoped<ICancionBussnies, CancionBussnies>();

builder.Services.AddScoped<IGeneroCancionRepository, GeneroCancionRepository>();
builder.Services.AddScoped<IGeneroCancionBussnies, GeneroCancionBussnies>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();