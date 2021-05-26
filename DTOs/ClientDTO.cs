using DesafioAPI.Models;

namespace DesafioAPI.DTOs
{
    public class ClientDTO
    {
        public string Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ClientAddress Address { get; set; }
    }
}