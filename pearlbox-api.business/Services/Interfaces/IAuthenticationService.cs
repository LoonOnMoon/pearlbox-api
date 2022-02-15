using pearlbox_api.business.DataTransferObjects;

namespace pearlbox_api.business.Services
{
    public interface IAuthenticationService
    {
        Task<string> SignUpUser(SignUpUserDetails userDetails);
        Task<string> SignInUser(SignInWithPasswordUserDetails userDetails);
    }
}