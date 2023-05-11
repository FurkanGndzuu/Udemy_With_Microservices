using IdentityModel;
using IdentityServer4.Validation;
using IdentityServerService.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServerService.API.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
           var User = await _userManager.FindByEmailAsync(context.UserName);
            if (User == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Your password or Email is incorrect" });
                context.Result.CustomResponse = errors;

                return;
            }

            var passwordcheck =await _userManager.CheckPasswordAsync(User, context.Password);

            if (passwordcheck)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Your password or Email is incorrect" });
                context.Result.CustomResponse = errors;

                return;
            }

            context.Result = new GrantValidationResult(User.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
