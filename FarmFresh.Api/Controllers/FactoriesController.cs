using System.Text.Json;
using FarmFresh.Api.DTOs;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class FactoriesController : BaseController<FactoriesController>
    {
        public FactoriesController(IConfiguration config, IUnitOfWork unitOfWork) : base(config, unitOfWork)
        {
        }

        /// <summary>
        /// Retrieve the current status of all factories
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No Found</response>
        [HttpGet]
        public async Task<ActionResult> GetFactories()
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();
            List<FactoryDto> factories = new();

            var orgFertJson = JsonSerializer.Serialize(user.OrganicFertilizerFactory);
            factories.Add(JsonSerializer.Deserialize<FactoryDto>(orgFertJson)!);

            var orgSeedsJson = JsonSerializer.Serialize(user.OrganicSeedsFactory);
            factories.Add(JsonSerializer.Deserialize<FactoryDto>(orgSeedsJson)!);

            var pestADisJson = JsonSerializer.Serialize(user.PestAndDiseaseFactory);
            factories.Add(JsonSerializer.Deserialize<FactoryDto>(pestADisJson)!);

            var soilAmendJson = JsonSerializer.Serialize(user.SoilAmendmentsFactory);
            factories.Add(JsonSerializer.Deserialize<FactoryDto>(soilAmendJson)!);

            return Ok(factories);
        }

        /// <summary>
        /// Activates a specific factory
        /// </summary>
        /// <param name="id">0 = Organic Fertilizer, 1 = Organic Seeds, 2 = Pest And Disease, 3 = Soil Amendments</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No Found</response>
        [HttpPatch("activate")]
        public async Task<ActionResult> ActivateFactory([FromQuery] int id)
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            switch (id)
            {
                case 0:
                    user.OrganicFertilizerFactory.Active = true;
                    break;
                case 1:
                    user.OrganicSeedsFactory.Active = true;
                    break;
                case 2:
                    user.PestAndDiseaseFactory.Active = true;
                    break;
                case 3:
                    user.SoilAmendmentsFactory.Active = true;
                    break;
                default:
                    return BadRequest("Invalid id");
            }

            await UnitOfWork.Complete();
            return Ok();
        }

        /// <summary>
        /// Deactivates a specific factory
        /// </summary>
        /// <param name="id">0 = Organic Fertilizer, 1 = Organic Seeds, 2 = Pest And Disease, 3 = Soil Amendments</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No Found</response>
        [HttpPatch("deactivate")]
        public async Task<ActionResult> DeactivateFactory([FromQuery] int id)
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            switch (id)
            {
                case 0:
                    user.OrganicFertilizerFactory.Active = false;
                    break;
                case 1:
                    user.OrganicSeedsFactory.Active = false;
                    break;
                case 2:
                    user.PestAndDiseaseFactory.Active = false;
                    break;
                case 3:
                    user.SoilAmendmentsFactory.Active = false;
                    break;
                default:
                    return BadRequest("Invalid id");
            }

            await UnitOfWork.Complete();
            return Ok();
        }
    }
}