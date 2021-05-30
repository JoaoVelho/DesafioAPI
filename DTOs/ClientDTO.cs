using DesafioAPI.Models;

namespace DesafioAPI.DTOs
{
    public class ClientDTO
    {
        public string Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
    }
}