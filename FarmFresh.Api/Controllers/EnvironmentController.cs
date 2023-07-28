using System.Text.Json;
using FarmFresh.Api.DTOs;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class EnvironmentController : BaseController<EnvironmentController>
    {
        private readonly SimulationEnvironment _simulationEnvironment;
        public EnvironmentController(IConfiguration config, IUnitOfWork unitOfWork, SimulationEnvironment simulationEnvironment) : base(config, unitOfWork)
        {
            _simulationEnvironment = simulationEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> GetEnvironment()
        {
            var user = await UnitOfWork.UserRepository.GetUserById(1);
            if (user == null) return NotFound();

            var envJson = JsonSerializer.Serialize(_simulationEnvironment);
            return Ok(JsonSerializer.Deserialize<EnvironmentDto>(envJson));
        }
    }
}