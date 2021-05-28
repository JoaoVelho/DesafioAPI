using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "Logradouro é necessário")]
        [MaxLength(100, ErrorMessage = "Logradouro muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Logradouro muito pequeno, deve ter mais que {1} caracteres")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Número do endereço é necessário")]
        [Range(1, Int32.MaxValue, ErrorMessage="Número do endereço inválido")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Complemento é necessário")]
        [MaxLength(100, ErrorMessage = "Complemento muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Complemento muito pequeno, deve ter mais que {1} caracteres")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "CEP é necessário")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Bairro é necessário")]
        [MaxLength(100, ErrorMessage = "Bairro muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Bairro muito pequeno, deve ter mais que {1} caracteres")]
        public string District { get; set; }

        [Required(ErrorMessage = "Município é necessário")]
        [MaxLength(100, ErrorMessage = "Município muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Município muito pequeno, deve ter mais que {1} caracteres")]
        public string City { get; set; }

        [Required(ErrorMessage = "Estado é necessário")]
        [MaxLength(100, ErrorMessage = "Estado muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Estado muito pequeno, deve ter mais que {1} caracteres")]
        public string State { get; set; }
    }
}