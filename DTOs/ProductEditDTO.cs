using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class ProductEditDTO
    {
        [Required(ErrorMessage = "Id é necessário")]
        [Range(1, Int32.MaxValue, ErrorMessage="Id inválido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é necessário")]
        [MaxLength(100, ErrorMessage = "Nome muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Nome muito pequeno, deve ter mais que {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição é necessária")]
        [MaxLength(200, ErrorMessage = "Descrição muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Descrição muito pequena, deve ter mais que {1} caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Unidade é necessária")]
        [MaxLength(100, ErrorMessage = "Unidade muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Unidade muito pequena, deve ter mais que {1} caracteres")]
        public string Unit { get; set; }
    }
}