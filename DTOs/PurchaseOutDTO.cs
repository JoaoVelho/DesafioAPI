using System.Collections.Generic;

namespace DesafioAPI.DTOs
{
    public class PurchaseOutDTO
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public ICollection<ItemDTO> PurchaseItems { get; set; }
    }
}