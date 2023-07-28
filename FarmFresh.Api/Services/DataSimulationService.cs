using FarmFresh.Api.Interfaces;
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

        public async Task Simulate(IUnitOfWork unitOfWork)
        {
            var users = await unitOfWork.UserRepository.GetAll();
            foreach (var user in users)
            {
                var orgFertFact = user.OrganicFertilizerFactory;
                var orgSeedsFact = user.OrganicSeedsFactory;
                var pNDFact = user.PestAndDiseaseFactory;
                var soilAFact = user.SoilAmendmentsFactory;

                var totalUsage = (orgFertFact.Active ? orgFertFact.PowerUsage : 0)
                                    + (orgSeedsFact.Active ? orgSeedsFact.PowerUsage : 0)
                                    + (pNDFact.Active ? pNDFact.PowerUsage : 0)
                                    + (soilAFact.Active ? soilAFact.PowerUsage : 0);
                var totalProduction = 0.0;

                totalProduction += _environment.Wind.Production;
                totalProduction += _environment.Solar.Production;

                // Calculate power plant power
                foreach (var coalPowerPlant in user.CoalPowerPlants)
                {
                    if (coalPowerPlant.Active)
                    {
                        totalProduction += coalPowerPlant.Production;
                        user.Balance -= coalPowerPlant.Price / 60 * 10;
                    }
                }

                // If usage exceeding production
                if (totalUsage > totalProduction) user.Balance -= _environment.PowerPrice / 60 * 10 * (totalUsage - totalProduction);

                // If usage lower than production
                if (totalUsage < totalProduction) user.Balance += _environment.PowerPrice / 60 * 10 * (totalProduction - totalUsage);

                // Add to factory capacities
                if (orgFertFact.Active && orgFertFact.Capacity < orgFertFact.MaxCapacity) orgFertFact.Capacity += orgFertFact.Production / 60 * 10;
                if (orgSeedsFact.Active && orgSeedsFact.Capacity < orgSeedsFact.MaxCapacity) orgSeedsFact.Capacity += orgSeedsFact.Production / 60 * 10;
                if (pNDFact.Active && pNDFact.Capacity < pNDFact.MaxCapacity) pNDFact.Capacity += pNDFact.Production / 60 * 10;
                if (soilAFact.Active && pNDFact.Capacity < pNDFact.MaxCapacity) soilAFact.Capacity += soilAFact.Production / 60 * 10;
            }
            await unitOfWork.Complete();
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
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                await Simulate(unitOfWork!);
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
                    Log.Information("Running data simulation service...");

                    StartSimulation();
                }, cancellationToken);
            }
        }
    }
}