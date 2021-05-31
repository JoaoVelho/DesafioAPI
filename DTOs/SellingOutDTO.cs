using System.Collections.Generic;

namespace DesafioAPI.DTOs
{
    public class SellingOutDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public ICollection<ItemDTO> SellingItems { get; set; }
        public bool Confirmed { get; set; }
    }
}