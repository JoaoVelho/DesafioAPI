using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAPI.Configurations
{
    public class SupplierAddressConfiguration : IEntityTypeConfiguration<SupplierAddress>
    {
        public void Configure(EntityTypeBuilder<SupplierAddress> builder)
        {
            builder.HasData(
                new SupplierAddress {
                    Id = 1,
                    Street = "Rua Faria Lima",
                    Number = 111,
                    Complement = "",
                    CEP = "523423412",
                    District = "Centro",
                    City = "Bauru",
                    State = "SP"
                },
                new SupplierAddress {
                    Id = 2,
                    Street = "Av Castelo Branco",
                    Number = 347,
                    Complement = "Apt 51",
                    CEP = "94656345",
                    District = "Rebouças",
                    City = "São Paulo",
                    State = "SP"
                }
            );
        }
    }
}