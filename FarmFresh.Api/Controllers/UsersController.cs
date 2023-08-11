using System.Text.Json;
using FarmFresh.Api.DTOs;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class UsersController : BaseController<UsersController>
    {
        private readonly SimulationEnvironment _simulationEnvironment;
        public UsersController(IConfiguration config, IUnitOfWork unitOfWork, SimulationEnvironment simulationEnvironment) : base(config, unitOfWork)
        {
            _simulationEnvironment = simulationEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
            var user = await UnitOfWork.UserRepository.GetUserByUid(extractedApiKey);
            if (user == null) return NotFound();

            var userJson = JsonSerializer.Serialize(user);
            return Ok(JsonSerializer.Deserialize<UserDto>(userJson));
        }

        [HttpPatch("storage/sell")]
        public async Task<ActionResult> Sell([FromQuery] int id)
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
            var user = await UnitOfWork.UserRepository.GetUserByUid(extractedApiKey);
            if (user == null) return NotFound();

            var price = _simulationEnvironment.SellPrices[id];
            switch (id)
            {
                case 0:
                    user.Balance += user.OrganicFertilizerFactory.Capacity * price;
                    user.OrganicFertilizerFactory.Capacity = 0;
                    break;
                case 1:
                    user.Balance += user.OrganicSeedsFactory.Capacity * price;
                    user.OrganicSeedsFactory.Capacity = 0;
                    break;
                case 2:
                    user.Balance += user.PestAndDiseaseFactory.Capacity * price;
                    user.PestAndDiseaseFactory.Capacity = 0;
                    break;
                case 3:
                    user.Balance += user.SoilAmendmentsFactory.Capacity * price;
                    user.SoilAmendmentsFactory.Capacity = 0;
                    break;
                default:
                    return BadRequest("Invalid id");
            }
            await UnitOfWork.Complete();
            return Ok();
        }
        [HttpPatch("factory/activate")]
        public async Task<ActionResult> ActivateFactory([FromQuery] int id)
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
            var user = await UnitOfWork.UserRepository.GetUserByUid(extractedApiKey);
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
        [HttpPatch("factory/deactivate")]
        public async Task<ActionResult> DeactivateFactory([FromQuery] int id)
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
            var user = await UnitOfWork.UserRepository.GetUserByUid(extractedApiKey);
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