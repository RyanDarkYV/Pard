using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pard.Application.Auth;
using Pard.Application.Helpers;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;
using Pard.Application.Models.Options;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.Login(model);
            if (!result.Result.Succeeded)
            {
                return BadRequest(result.Result.Errors);
            }
            return new OkObjectResult(result.JwtToken);
        }
    }
}