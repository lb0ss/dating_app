using System.ComponentModel.DataAnnotations;

namespace dating_app.api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]  // data annotation
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "The password must be between 4 and 8 characters")]
        public string Password { get; set; }
    }
}