namespace DesafioAPI.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public SupplierAddress Address { get; set; }
        public int AddressId { get; set; }
    }
}