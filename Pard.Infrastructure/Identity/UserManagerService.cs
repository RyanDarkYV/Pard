using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pard.Application.Common.Interfaces;
using Pard.Application.Models;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;

namespace Pard.Infrastructure.Identity
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserManagerService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(RegistrationViewModel model)
        {
            var userIdentity = _mapper.Map<AppUser>(model);
            userIdentity.JoinDate = DateTime.UtcNow;

            var identityResult = await _userManager.CreateAsync(userIdentity, model.Password);

            var result = (identityResult.ToApplicationResult(), userIdentity.Id);
            if (result.Item1.Succeeded)
            {
                await _userManager.AddToRoleAsync(userIdentity, "User");
                if (model.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(userIdentity,"Admin");
                }
                if (model.IsSuperAdmin)
                {
                    await _userManager.AddToRoleAsync(userIdentity,"Superadmin");
                }
            }

            return result;
        }
        
    }
}