using TrackIt.API.Extensions;
using TrackIt.API.Middleware;
using TrackIt.Application;
using TrackIt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<UserContextMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseInfrastructureServices();
app.ConfigureSwagger();
app.UseHttpsRedirection();

app.Run();