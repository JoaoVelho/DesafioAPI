using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class StockEditDTO
    {
        [Required(ErrorMessage = "Id é necessário")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Valor de venda é necessário")]
        [Range(0.01, float.MaxValue, ErrorMessage="Valor de venda inválido")]
        public float SellValue { get; set; }
    }
}