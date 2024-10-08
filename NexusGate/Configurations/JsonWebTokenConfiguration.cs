﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace NexusGate.Configurations;

public static class JsonWebTokenConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = _tokenValidationParameters();
            });

        return services;
    }

    private static TokenValidationParameters _tokenValidationParameters()
    {
        var key = Environment.GetEnvironmentVariable("JWT_KEY") ??
                  throw new InvalidOperationException("JWT_KEY NOT SET.");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var validIssuer = Environment.GetEnvironmentVariable("JWT_VALID_ISSUER") ??
                          throw new InvalidOperationException("JWT_VALID_ISSUER NOT SET.");
        var validAudience = Environment.GetEnvironmentVariable("JWT_VALID_AUDIENCE") ??
                            throw new InvalidOperationException("JWT_VALID_AUDIENCE NOT SET.");

        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero,
        };

        return tokenValidationParameter;
    }
}