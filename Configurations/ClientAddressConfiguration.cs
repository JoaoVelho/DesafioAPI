using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAPI.Configurations
{
    public class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            builder.HasData(
                new ClientAddress {
                    Id = 1,
                    Street = "Rua Osvaldo da Cruz",
                    Number = 233,
                    Complement = "",
                    CEP = "423921837",
                    District = "Centro",
                    City = "Sorocaba",
                    State = "SP"
                },
                new ClientAddress {
                    Id = 2,
                    Street = "Av Nações Unidas",
                    Number = 1245,
                    Complement = "Apt 302",
                    CEP = "123463456",
                    District = "Vila Aviação",
                    City = "Curitiba",
                    State = "PR"
                }
            );
        }
    }
}