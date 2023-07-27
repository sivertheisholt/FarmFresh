using FarmFresh.Api.DTOs;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class UserController : BaseController<UserController>
    {
        public UserController(IConfiguration config, IUnitOfWork unitOfWork) : base(config, unitOfWork)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUserDto newUserDto)
        {
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