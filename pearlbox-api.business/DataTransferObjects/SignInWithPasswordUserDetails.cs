using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.business.DataTransferObjects
{
    public class SignInWithPasswordUserDetails
    {
        [Required]
        public string EmailOrUserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}