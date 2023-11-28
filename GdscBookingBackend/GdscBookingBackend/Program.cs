using GdscBookingBackend.Data;
using GdscBookingBackend.Swagger;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var keycloakOptions = configuration
    .GetRequiredSection(ConfigurationConstants.ConfigurationPrefix)
    .Get<KeycloakInstallationOptions>();

if (keycloakOptions is null) throw new Exception("keyCloakAdminConfiguartions is null");

builder.Services.AddSwaggerConfiguration(keycloakOptions);
builder.Services.AddKeycloakAuthentication(configuration);
builder.Services.AddAuthorization(
    o => o.AddPolicy("Admin", b => { b.RequireRealmRoles("Admin");}));
builder.Services.AddKeycloakAuthorization(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();