using Serilog;

namespace FarmFresh.Api.Services
{
    public class DataSimulationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly SimulationEnvironment _environment;
        public DataSimulationService(IServiceScopeFactory scopeFactory, IConfiguration config, SimulationEnvironment environment)
        {
            _environment = environment;
            _config = config;
            _scopeFactory = scopeFactory;
        }

        public Task Simulate()
        {

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
                await RunInBackground(TimeSpan.FromSeconds(1), () =>
                {
                    Log.Information("Running data simulation service...");

                    StartSimulation();
                }, cancellationToken);
            }
        }
    }
}