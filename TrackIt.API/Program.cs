using TrackIt.API.Extensions;
using TrackIt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddLogging(builder.Configuration);

var app = builder.Build();

app.Services.ExecuteDatabaseSeed();
app.ConfigureSwagger();
app.UseHttpsRedirection();

app.Run();