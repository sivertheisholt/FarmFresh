using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }
        protected IConfiguration Config { get { return _config; } }
        protected IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
    }
}