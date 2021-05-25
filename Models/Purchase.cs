using System.Collections.Generic;

namespace DesafioAPI.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}