using System.ComponentModel.DataAnnotations;
using DesafioAPI.Validations;

namespace DesafioAPI.DTOs
{
    public class UserRegisterDTO : UserLoginDTO
    {
        [Required(ErrorMessage = "Confirmação de senha é necessária")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Nome é necessário")]
        [MaxLength(100, ErrorMessage = "Nome muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Nome muito pequeno, deve ter mais que {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF é necessário")]
        [CPF]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Telefone é necessário")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Endereço é necessário")]
        public AddressDTO Address { get; set; }
    }
}