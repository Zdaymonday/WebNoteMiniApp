using System.ComponentModel.DataAnnotations;

namespace WebNoteMiniApp.Auth
{
    public record class UserDto(string username, string password);

    public record UserModel
    {
        [Required]
        public string UserName { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}
