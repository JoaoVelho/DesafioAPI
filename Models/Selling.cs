using System.Collections.Generic;

namespace DesafioAPI.Models
{
    public class Selling
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }
        public ICollection<SellingItem> SellingItems { get; set; }
    }
}