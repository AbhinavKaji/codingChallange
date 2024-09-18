using Microsoft.AspNetCore.Mvc;
using Rockfast.Dependencies;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;

namespace Rockfast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<UserVM> Post(UserVM model)
        {
            try
            {
                return await _userService.CreateUserAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<UserVM>> GetAll()
        {
            try
            {
                return await _userService.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Enumerable.Empty<UserVM>();
            }
        }
    }
}
