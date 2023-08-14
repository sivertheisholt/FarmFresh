using System.Text.Json;
using FarmFresh.Api.DTOs;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class UsersController : BaseController<UsersController>
    {
        public UsersController(IConfiguration config, IUnitOfWork unitOfWork) : base(config, unitOfWork)
        {
        }

        /// <summary>
        /// Retrieve user information
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No Found</response>
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            User? user = await GetCurrentUser();
            if (user == null) return NotFound();

            var userJson = JsonSerializer.Serialize(user);
            return Ok(JsonSerializer.Deserialize<UserDto>(userJson));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUserDto newUserDto)
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
            if (!extractedApiKey.Equals(Config["ApiKeyAdmin"])) return Unauthorized();
            var plants = new List<CoalPowerPlant>()
            {
                new CoalPowerPlant(),
                new CoalPowerPlant(),
                new CoalPowerPlant(),
                new CoalPowerPlant()
            };
            var organicFertilizerFactory = new OrganicFertilizerFactory();
            var organicSeedsFactory = new OrganicSeedsFactory();
            var pestAndDiseaseFactory = new PestAndDiseaseFactory();
            var soilAmendmentsFactory = new SoilAmendmentsFactory();

            var user = new User()
            {
                UserUUID = newUserDto.UUID,
                GroupName = newUserDto.GroupName,
                CoalPowerPlants = plants,
                OrganicFertilizerFactory = organicFertilizerFactory,
                OrganicSeedsFactory = organicSeedsFactory,
                PestAndDiseaseFactory = pestAndDiseaseFactory,
                SoilAmendmentsFactory = soilAmendmentsFactory
            };

            foreach (var plant in plants)
            {
                UnitOfWork.CoalPowerPlantRepository.Add(plant);
            }
            UnitOfWork.OrganicFertilizerFactoryRepository.Add(organicFertilizerFactory);
            UnitOfWork.OrganicSeedsFactoryRepository.Add(organicSeedsFactory);
            UnitOfWork.PestAndDiseaseFactoryRepository.Add(pestAndDiseaseFactory);
            UnitOfWork.SoilAmendmentsFactoryRepository.Add(soilAmendmentsFactory);

            UnitOfWork.UserRepository.Add(user);

            await UnitOfWork.Complete();
            return Ok();
        }
    }
}