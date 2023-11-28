using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace GdscBookingBackend.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        KeycloakInstallationOptions keycloakOption)
    {
        var url = $"{keycloakOption.KeycloakUrlRealm}/protocol/openid-connect";
        
        var openIdConnectSecurityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Type = SecuritySchemeType.OAuth2,
            In = ParameterLocation.Header,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(url + "/auth"),
                    TokenUrl = new Uri(url + "/token"),
                    Scopes = new Dictionary<string, string> { { "openid", "openid" }, { "profile", "profile" } }
                }
            }
        };
        
        var securityRequirements = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "gdsc"
                    }
                },
                Array.Empty<string>()
            }
        };
        
        return services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("gdsc", openIdConnectSecurityScheme);
            option.AddSecurityRequirement(securityRequirements);
        });
        
    }
}