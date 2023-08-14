using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        public ActionResult GetSolar()
        {
            return Ok(_simulationEnvironment.Solar);
        }
        [HttpGet("wind")]
        public ActionResult GetWind()
        {
            return Ok(_simulationEnvironment.Wind);
        }
        [HttpGet("coal")]
        public async Task<ActionResult> GetCoalPowerPlants()
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            return Ok(user.CoalPowerPlants);
        }

        [HttpPatch("coal/activate")]
        public async Task<ActionResult> ActivateCoalPlant([FromQuery] int id)
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            try
            {
                user.CoalPowerPlants[id].Active = true;
                await UnitOfWork.Complete();
            }
            catch (Exception e)
            {
                Log.Error(e, "Invalid id");
                return BadRequest("Invalid id");
            }

            return Ok();
        }
        [HttpPatch("coal/deactivate")]
        public async Task<ActionResult> DeactivateCoalPlant([FromQuery] int id)
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            try
            {
                user.CoalPowerPlants[id].Active = false;
                await UnitOfWork.Complete();
            }
            catch (Exception e)
            {
                Log.Error(e, "Invalid id");
                return BadRequest("Invalid id");
            }

            return Ok();
        }
    }
}