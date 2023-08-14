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

        /// <summary>
        /// Retrieve the current status of solar panels
        /// </summary>
        [HttpGet("solar")]
        public ActionResult GetSolar()
        {
            return Ok(_simulationEnvironment.Solar);
        }

        /// <summary>
        /// Retrieve the current status of wind mills
        /// </summary>
        [HttpGet("wind")]
        public ActionResult GetWind()
        {
            return Ok(_simulationEnvironment.Wind);
        }

        /// <summary>
        /// Retrieve the current status of all coal power plants
        /// </summary>
        [HttpGet("coal")]
        public async Task<ActionResult> GetCoalPowerPlants()
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            return Ok(user.CoalPowerPlants);
        }

        /// <summary>
        /// Activates a coal power plant
        /// </summary>
        /// <param name="id">Coal power plant index [0-3]</param>
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
        /// <summary>
        /// Deactivates a coal power plant
        /// </summary>
        /// <param name="id">Coal power plant index [0-3]</param>
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