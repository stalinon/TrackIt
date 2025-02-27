using TrackIt.API.Extensions;
using TrackIt.API.Middleware;
using TrackIt.Application;
using TrackIt.Infrastructure;
using TrackIt.TelegramBot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddTelegramBotServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.ConfigureSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseInfrastructureServices();
app.UseTelegramBotServices();
app.MapControllers();

app.UseMiddleware<UserContextMiddleware>();
app.Run();