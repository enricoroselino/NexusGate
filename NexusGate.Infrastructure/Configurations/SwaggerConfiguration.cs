using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NexusGate.Infrastructure.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddApiDocumentationConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddSwaggerGenWithAuthentication();
        services.ConfigureOptions<SwaggerGenConfigureOptions>();
        return services;
    }

    public static IApplicationBuilder UseApiDocumentationConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
    
    private static void AddSwaggerGenWithAuthentication(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Description = "Please enter your JWT with this format: ''YOUR_TOKEN''",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        };

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(securityRequirement);
        });
    }
}

internal class SwaggerGenConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerGenConfigureOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }
    
    public void Configure(SwaggerGenOptions options)
    {
        const string projectName = nameof(NexusGate);
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = $"{projectName}.v{description.ApiVersion}",
                Version = description.ApiVersion.ToString()
            };
            
            options.SwaggerDoc(description.GroupName, openApiInfo);
        }
    }
}