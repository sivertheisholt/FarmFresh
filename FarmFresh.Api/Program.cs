using FarmFresh.Api;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    await Startup.ConfigureLogger(builder);

    await Startup.ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();

    await Startup.ConfigureDatabase(app);

    await Startup.ConfigureMiddleWare(app);

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}