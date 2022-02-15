using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.business.DataTransferObjects
{
    public class SignUpUserDetails
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}