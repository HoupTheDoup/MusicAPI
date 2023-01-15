using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Web.Models.Users
{
    public class RegisterUserModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
