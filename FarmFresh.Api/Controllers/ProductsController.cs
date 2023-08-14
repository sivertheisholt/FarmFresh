using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    public class ProductsController : BaseController<ProductsController>
    {
        private readonly SimulationEnvironment _simulationEnvironment;
        public ProductsController(IConfiguration config, IUnitOfWork unitOfWork, SimulationEnvironment simulationEnvironment) : base(config, unitOfWork)
        {
            _simulationEnvironment = simulationEnvironment;
        }

        /// <summary>
        /// Sell specific product
        /// </summary>
        /// <param name="id">0 = Organic Fertilizer, 1 = Organic Seeds, 2 = Pest And Disease, 3 = Soil Amendments</param>
        [HttpPatch("sell")]
        public async Task<ActionResult> Sell([FromQuery] int id)
        {
            User? user = await GetCurrentUser();
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
    }
}