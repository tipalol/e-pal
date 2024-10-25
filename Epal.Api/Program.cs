using Epal.Api.Configuration;
using Epal.Application.Configuration;
using Epal.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .ConfigureApplication()
    .ConfigureInfrastructure(builder.Configuration)
    .ConfigureApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureAppPipeline();

app.Run();