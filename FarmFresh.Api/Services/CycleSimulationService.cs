using Serilog;

namespace FarmFresh.Api.Services
{
    public class CycleSimulationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;
        private readonly SimulationEnvironment _environment;
        public CycleSimulationService(IServiceScopeFactory scopeFactory, IConfiguration config, SimulationEnvironment environment)
        {
            _environment = environment;
            _config = config;
            _scopeFactory = scopeFactory;
        }

        public Task Simulate()
        {
            Random random = new();
            _environment.Day = !_environment.Day;

            if (_environment.Day)
            {
                _environment.SellPrices = new List<int>()
                {
                    random.Next() * 100,
                    random.Next() * 100,
                    random.Next() * 100,
                    random.Next() * 100
                };
            }

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
                Log.Error(e, "Could not simulate cycle");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await RunInBackground(TimeSpan.FromSeconds(720), () =>
                {
                    Log.Information("Running cycle simulation service...");

                    StartSimulation();
                }, cancellationToken);
            }
        }
    }
}