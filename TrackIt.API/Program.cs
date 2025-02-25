using TrackIt.API.Extensions;
using TrackIt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Services.ExecuteDatabaseSeed();
app.ConfigureSwagger();
app.UseHttpsRedirection();

app.Run();