using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pard.Application.Auth;
using Pard.Application.Common.Interfaces;
using Pard.Application.Helpers;
using Pard.Application.Models;
using Pard.Application.Models.Options;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Identity;

namespace Pard.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public AuthService(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        public async Task<(Result Result, string JwtToken)> Login(CredentialsViewModel model)
        {
            var identity = await GetClaimsIdentity(model.UserName, model.Password);
            if (identity == null)
            {
                var errors = new List<string> {"invalid username or password."};
                return (Result.Failure(errors), string.Empty);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.UserName, _jwtIssuerOptions,
                new JsonSerializerSettings {Formatting = Formatting.Indented});

            return (Result.Success(), jwt);
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