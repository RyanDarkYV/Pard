using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Pard.Application.Common.Interfaces;
using Pard.Application.ViewModels;
using System.Threading.Tasks;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class AccountsController : BaseController
    {
        private readonly IUserManager _userService;

        public AccountsController(IUserManager userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns UserId</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(model);

            if (result.Result.Succeeded)
                return new OkObjectResult(result.UserId);
            
            
            return new BadRequestObjectResult(result.Result.Errors);
        }
    }
}