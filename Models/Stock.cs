namespace DesafioAPI.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float SellValue { get; set; }
    }
}