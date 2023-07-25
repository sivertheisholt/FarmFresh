using FarmFresh.Api.Data;
using FarmFresh.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmFresh.Api
{
    public static class Startup
    {
        public static Task ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<SimulationEnvironment>();
            services.AddHostedService<ProductionSimulationService>();
            services.AddHostedService<EnvironmentSimulationService>();

            return Task.CompletedTask;
        }
        public static async Task ConfigureDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DataContext>();

            await context.Database.MigrateAsync();
        }
        public static Task ConfigureMiddleWare(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return Task.CompletedTask;
        }

        public static Task ConfigureLogger(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            return Task.CompletedTask;
        }
    }
}