using System.Text.Json.Serialization;

namespace DesafioAPI.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Purchase Purchase { get; set; }
        public int PurchaseId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public float Value { get; set; }
    }
}