using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using pearlbox_api.business.DataTransferObjects;
using pearlbox_api.business.Exceptions.Authentication;
using pearlbox_api.data.DatabaseObjects.Authentication;

namespace pearlbox_api.business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfigurationRoot configuration;
        public AuthenticationService(
            UserManager<User> userManager,
            IConfigurationRoot configuration
        )
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<string> SignUpUser(SignUpUserDetails userDetails){
            var user = await GetUser(userDetails.Email, userDetails.UserName);
            if (user != null)
                throw new SignUpUserExistsException(userDetails.Email, userDetails.UserName);
            
            user = new User() { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await userManager.CreateAsync(user, userDetails.Password);
            if (!result.Succeeded)
                throw new SignUpFailedException();

            var authClaims = await GetAuthClaims(user);

            var token = GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> SignInUser(SignInWithPasswordUserDetails userDetails){
            var user = await GetUser(userDetails.EmailOrUserName, userDetails.EmailOrUserName);
            if (user == null)
                throw new SignInUserDoesNotExistException(userDetails.EmailOrUserName);

            bool correctPassword = await userManager.CheckPasswordAsync(user, userDetails.Password);
            if (!correctPassword)
                throw new SignInIncorrectPasswordException(userDetails.EmailOrUserName);

            var authClaims = await GetAuthClaims(user);

            var token = GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<List<Claim>> GetAuthClaims(User user){
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            return authClaims;
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private async Task<User?> GetUser(string? email, string? username){
            var user = await userManager.FindByEmailAsync(email);
            if (user == null){
                user = await userManager.FindByNameAsync(username);
                if (user == null) return null;
            }
            return user;
        }
    }
}