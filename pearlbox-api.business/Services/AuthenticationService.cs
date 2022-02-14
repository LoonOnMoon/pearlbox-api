using Microsoft.AspNetCore.Identity;
using pearlbox_api.business.DataTransferObjects;

namespace pearlbox_api.business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthenticationService(
            UserManager<IdentityUser> userManager
        )
        {
            //this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task RegisterUser(UserDetails userDetails){
            var identityUser = new IdentityUser() { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {

            }
        }
    }
}