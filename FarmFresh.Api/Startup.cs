using FarmFresh.Api.Attributes;
using FarmFresh.Api.Data;
using FarmFresh.Api.Interfaces;
using FarmFresh.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FarmFresh.Api
{
    public static class Startup
    {
        public static Task ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<CustomHeaderSwaggerAttribute>();
            });

            services.AddHttpContextAccessor();

            services.AddDbContext<DataContext>(options =>
            {
                string connStr;

                if (env == "Development")
                {
                    connStr = config.GetConnectionString("DefaultConnection")!;
                }
                else
                {
                    connStr = Environment.GetEnvironmentVariable("DB_CONN_STR")!;
                }
                options.UseSqlServer(connStr);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<SimulationEnvironment>();
            services.AddHostedService<CycleSimulationService>();
            services.AddHostedService<ProductionSimulationService>();
            services.AddHostedService<EnvironmentSimulationService>();
            services.AddHostedService<DataSimulationService>();

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
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

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