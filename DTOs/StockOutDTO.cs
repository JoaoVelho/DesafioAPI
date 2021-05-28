namespace DesafioAPI.DTOs
{
    public class StockOutDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public float SellValue { get; set; }
    }
}