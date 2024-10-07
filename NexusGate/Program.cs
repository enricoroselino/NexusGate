using System.Reflection;
using Asp.Versioning;
using Carter;
using DotNetEnv;
using NexusGate;
using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;
using NexusGate.Modules;

Env.Load(".env");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigurationsBootstrap(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseRateLimiterConfiguration();
app.UseEndpointConfiguration();
await app.RunAsync();