using Serilog;

namespace FarmFresh.Api.Services
{
    public class ProductionSimulationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly SimulationEnvironment _environment;
        public ProductionSimulationService(IServiceScopeFactory scopeFactory, IConfiguration config, SimulationEnvironment environment)
        {
            _environment = environment;
            _config = config;
            _scopeFactory = scopeFactory;
        }

        public Task Simulate()
        {
            Random random = new();

            // Wind
            if (_environment.Windy) _environment.Wind.Production = random.NextDouble() * (30.0 - 20) + 20;
            else _environment.Wind.Production = random.NextDouble() * 15.0;

            // Solar
            if (_environment.Day)
            {
                if (_environment.Sunny) _environment.Solar.Production = random.NextDouble() * (20.0 - 10) + 10;
                else _environment.Solar.Production = random.NextDouble() * 10;
            }

            Log.Information("Daytime: " + _environment.Day.ToString());
            Log.Information("Solar production: " + _environment.Solar.Production.ToString());
            Log.Information("Wind production: " + _environment.Wind.Production.ToString());

            return Task.CompletedTask;
        }
        private static async Task RunInBackground(TimeSpan timeSpan, Action action, CancellationToken cancellationToken)
        {
            var periodicTimer = new PeriodicTimer(timeSpan);
            while (await periodicTimer.WaitForNextTickAsync(cancellationToken))
            {
                if (cancellationToken.IsCancellationRequested) return;
                action();
            }
        }

        private async void StartSimulation()
        {
            using var scope = _scopeFactory.CreateScope();
            try
            {
                await Simulate();
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not simulate production");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await RunInBackground(TimeSpan.FromSeconds(10), () =>
                {
                    Log.Information("Running production simulation service...");

                    StartSimulation();
                }, cancellationToken);
            }
        }
    }
}