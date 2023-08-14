using FarmFresh.Api.Attributes;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FarmFresh.Api.Controllers
{
    [ApiKey]
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
        protected async Task<User?> GetCurrentUser()
        {
            try
            {
                HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);
                var user = await UnitOfWork.UserRepository.GetUserByUid(extractedApiKey!);
                return user;
            }
            catch (Exception e)
            {
                Log.Error(e, "Something went wrong when getting current user");
                return null;
            }
        }
    }
}