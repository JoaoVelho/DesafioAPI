using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email é necessário")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é necessária")]
        public string Password { get; set; }
    }
}