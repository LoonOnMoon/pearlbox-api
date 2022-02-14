using pearlbox_api.business.DataTransferObjects;

namespace pearlbox_api.business.Services
{
    public interface IAuthenticationService
    {
        Task RegisterUser(UserDetails userDetails);
    }
}