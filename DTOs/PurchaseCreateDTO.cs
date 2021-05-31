using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class PurchaseCreateDTO
    {
        [Required(ErrorMessage = "Id do fornecedor é necessário")]
        [Range(1, Int32.MaxValue, ErrorMessage="Id do fornecedor inválido")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Itens da compra são necessários")]
        public ICollection<ItemDTO> PurchaseItems { get; set; }
    }
}