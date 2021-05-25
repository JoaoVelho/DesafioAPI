using Microsoft.AspNetCore.Identity;

namespace DesafioAPI.Models
{
    public class Client : IdentityUser
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}