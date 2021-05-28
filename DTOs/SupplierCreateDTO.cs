using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class SupplierCreateDTO
    {
        [Required(ErrorMessage = "CNPJ é necessário")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Nome é necessário")]
        [MaxLength(100, ErrorMessage = "Nome muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Nome muito pequeno, deve ter mais que {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefone é necessário")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email é necessário")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Endereço é necessário")]
        public AddressDTO Address { get; set; }
    }
}