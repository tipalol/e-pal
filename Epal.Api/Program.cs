using Epal.Application.Features.Users.Get;
using Epal.Application.Interfaces;
using Epal.Infrastructure.Database;
using Epal.Infrastructure.Database.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(b =>
{
    b.RegisterServicesFromAssemblyContaining<GetUserRequest>();
});

builder.Services.AddDbContext<IEpalDbContext, EpalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!));

if (args.Length > 0 && args[0] == "migrate")
{
    var factory = CreateDbContextFactory(builder.Configuration);
    using var dbContext = factory.CreateDbContext(args.Skip(1).ToArray());
    dbContext.Database.Migrate();
    return;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDbContext(app);

app.Run();

static void MigrateDbContext(IHost app)
{
    using var scope = app.Services.CreateScope();
    var databaseFacade = scope.ServiceProvider.GetRequiredService<EpalDbContext>().Database;

    var migrations = databaseFacade.GetPendingMigrations().ToArray();
    if (migrations.Length > 0)
    {
        Console.WriteLine(
            $"Migrate database: {Environment.NewLine}{string.Join(Environment.NewLine, migrations)}");
    }

    databaseFacade.Migrate();
}

static IDesignTimeDbContextFactory<EpalDbContext> CreateDbContextFactory(IConfiguration configuration)
{
    return new EpalDbContextFactory(configuration);
}