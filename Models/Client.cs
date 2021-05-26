using Microsoft.AspNetCore.Identity;

namespace DesafioAPI.Models
{
    public class Client : IdentityUser
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public ClientAddress Address { get; set; }
        public int AddressId { get; set; }
    }
}