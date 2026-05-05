using System.ComponentModel.DataAnnotations;

namespace SitoWebRequiem.SharedModels
{
    public class SignUpDto
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
