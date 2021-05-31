using System.Text.Json.Serialization;

namespace DesafioAPI.Models
{
    public class SellingItem
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Selling Selling { get; set; }
        public int SellingId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public float Value { get; set; }
    }
}