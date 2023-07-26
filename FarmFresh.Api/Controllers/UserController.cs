using System.Reflection;
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
            var factories = new List<BaseFactory>()
            {
                new OrganicFertilizerFactory(),
                new OrganicSeedsFactory(),
                new PestAndDiseaseFactory(),
                new SoilAmendmentsFactory()
            };

            var user = new User()
            {
                UserUUID = newUserDto.UUID,
                GroupName = newUserDto.GroupName,
                Factories = factories,
                CoalPowerPlants = plants
            };

            foreach (var plant in plants)
            {
                UnitOfWork.CoalPowerPlantRepository.Add(plant);
            }
            foreach (var factory in factories)
            {
                Type unitOfWorkType = UnitOfWork.GetType();
                Type factoryType = factory.GetType();

                PropertyInfo? propertyInfo = unitOfWorkType.GetProperty(factoryType.Name + "Repository");
                if (propertyInfo != null)
                {
                    object? repository = propertyInfo.GetValue(unitOfWorkType); // Get the InnerObject
                    if (repository != null)
                    {
                        Type innerType = repository.GetType();
                        MethodInfo? innerProperty = innerType.GetMethod("Add");
                        innerProperty?.Invoke(innerType, new object[] { factory });
                    }
                }
            }

            UnitOfWork.UserRepository.Add(user);
            await UnitOfWork.Complete();
            return Ok();
        }
    }
}