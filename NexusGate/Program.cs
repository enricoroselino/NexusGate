using DotNetEnv;
using NexusGate;

Env.Load(".env");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfigurationsBootstrap();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseConfigurationsBootstrap();
await app.RunAsync();