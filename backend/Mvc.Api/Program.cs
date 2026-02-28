using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories y Bussnies
// Persona
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();

// Persona Tipo Documento
builder.Services.AddScoped<IPersonaTipoDocumentoRepository, PersonaTipoDocumentoRepository>();
builder.Services.AddScoped<IPersonaTipoDocumentoBussnies, PersonaTipoDocumentoBussnies>();

// Canción
builder.Services.AddScoped<ICancionRepository, CancionRepository>();
builder.Services.AddScoped<ICancionBussnies, CancionBussnies>();

// Género Canción
builder.Services.AddScoped<IGeneroCancionRepository, GeneroCancionRepository>();
builder.Services.AddScoped<IGeneroCancionBussnies, GeneroCancionBussnies>();

// Configurar CORS
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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
