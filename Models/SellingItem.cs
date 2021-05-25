namespace DesafioAPI.Models
{
    public class SellingItem
    {
        public int Id { get; set; }
        public Selling Selling { get; set; }
        public int SellingId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
    }
}