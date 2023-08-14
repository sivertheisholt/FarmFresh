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
    }
}