using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class ItemDTO
    {
        [Required(ErrorMessage = "Id do produto é necessário")]
        [Range(1, Int32.MaxValue, ErrorMessage="Id do produto inválido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantidade é necessária")]
        [Range(0.01, float.MaxValue, ErrorMessage="Quantidade inválida")]
        public float Quantity { get; set; }

        [Required(ErrorMessage = "Valor é necessário")]
        [Range(0.01, float.MaxValue, ErrorMessage="Valor inválido")]
        public float Value { get; set; }
    }
}