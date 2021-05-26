using DesafioAPI.Models;

namespace DesafioAPI.DTOs
{
    public class UserRegisterDTO : UserLoginDTO
    {
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public ClientAddress Address { get; set; }
    }
}