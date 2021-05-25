namespace DesafioAPI.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public Purchase Purchase { get; set; }
        public int PurchaseId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }
    }
}