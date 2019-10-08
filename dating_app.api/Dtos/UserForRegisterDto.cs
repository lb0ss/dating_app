using System;
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
        [Required]
        public string gender { get; set; }
        [Required]
        public string knownAs { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [Required]
        public string city { get; set; } 
        [Required]
        public string country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}