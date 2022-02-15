using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.web.Models.FormModels.Authentication
{
    public class SignInWithPasswordFormModel
    {
        [Required]
        public string? EmailOrUserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}