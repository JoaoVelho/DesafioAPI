using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.DTOs
{
    public class SellingCreateDTO
    {
        [Required(ErrorMessage = "Itens da venda são necessários")]
        public ICollection<ItemDTO> SellingItems { get; set; }
    }
}