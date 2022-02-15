using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.web.Models.FormModels.Authentication
{
    public class SignUpFormModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}