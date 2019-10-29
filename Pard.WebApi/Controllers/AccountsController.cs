using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pard.Application.Helpers;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;
using Pard.Persistence.Contexts;
using ILogger = Serilog.ILogger;

namespace Pard.WebApi.Controllers
{
    [EnableCors("Cors")]
    [Route("api/[controller]/[action]")]
    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IdentityContext _context;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, IdentityContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = dbContext;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = _mapper.Map<AppUser>(model);
            userIdentity.JoinDate = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }

            await _userManager.AddToRoleAsync(userIdentity, "User");
            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(userIdentity,"Admin");
            }
            if (model.IsSuperAdmin)
            {
                await _userManager.AddToRoleAsync(userIdentity,"Superadmin");
            }

            var rolesList = await _userManager.GetRolesAsync(userIdentity);
            await _context.SaveChangesAsync();
            return new OkObjectResult(model);
        }
    }
}