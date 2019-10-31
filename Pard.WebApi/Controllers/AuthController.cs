using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pard.API.Models.Identity;
using Pard.Application.Auth;
using Pard.Application.Helpers;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class AuthController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtIssuerOptions, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
            _jwtFactory = jwtFactory;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole {Name = "Superadmin"});
            await _roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
            await _roleManager.CreateAsync(new IdentityRole {Name = "User"});
            return new 
                OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(model.UserName, model.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "invalid username or password.",
                    ModelState));
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.UserName, _jwtIssuerOptions,
                new JsonSerializerSettings {Formatting = Formatting.Indented});

            

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string credentialsUserName, string credentialsPassword)
        {
            if (string.IsNullOrEmpty(credentialsUserName) || string.IsNullOrEmpty(credentialsPassword))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }
            

            var userToVerify = await _userManager.FindByNameAsync(credentialsUserName);
            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);


            if (await _userManager.CheckPasswordAsync(userToVerify, credentialsPassword))
            {
                var roles = await _userManager.GetRolesAsync(userToVerify);
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(credentialsUserName, userToVerify.Id, roles));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}