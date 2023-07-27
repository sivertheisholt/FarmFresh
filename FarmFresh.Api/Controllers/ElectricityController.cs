using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class ElectricityController : BaseController<ElectricityController>
    {
        private readonly SimulationEnvironment _simulationEnvironment;
        public ElectricityController(IConfiguration config, IUnitOfWork unitOfWork, SimulationEnvironment simulationEnvironment) : base(config, unitOfWork)
        {
            _simulationEnvironment = simulationEnvironment;
        }

        [HttpGet("solar")]
        public async Task<ActionResult> GetSolar()
        {
            return Ok(_simulationEnvironment.Solar);
        }
        [HttpGet("wind")]
        public async Task<ActionResult> GetWind()
        {
            return Ok(_simulationEnvironment.Wind);
        }
        [HttpGet("coal")]
        public async Task<ActionResult> GetCoalPowerPlants()
        {
            var user = await UnitOfWork.UserRepository.GetUserById(1);
            if (user == null) return NotFound();

            return Ok(user.CoalPowerPlants);
        }

    }
}