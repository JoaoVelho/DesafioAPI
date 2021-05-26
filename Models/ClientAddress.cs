namespace DesafioAPI.Models
{
    public class ClientAddress
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string CEP { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}