using Serilog;

namespace FarmFresh.Api.Services
{
    public class EnvironmentSimulationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly SimulationEnvironment _environment;
        public EnvironmentSimulationService(IServiceScopeFactory scopeFactory, IConfiguration config, SimulationEnvironment environment)
        {
            _environment = environment;
            _config = config;
            _scopeFactory = scopeFactory;
        }
        public Task Simulate()
        {
            Random random = new();
            _environment.Windy = random.Next(2) == 0;

            if (_environment.Day) _environment.Sunny = random.Next(2) == 0;
            else _environment.Sunny = false;

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
                Log.Error(e, "Could not simulate environment");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await RunInBackground(TimeSpan.FromSeconds(60), () =>
                {
                    Log.Information("Running environment simulation service...");

                    StartSimulation();
                }, cancellationToken);
            }
        }
    }
}