using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAPI.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasData(
                new Supplier {
                    Id = 1,
                    CNPJ = "06129000000172",
                    Name = "Joaquim",
                    Phone = "654645612",
                    Email = "joaquim@email.com",
                    AddressId = 1
                },
                new Supplier {
                    Id = 2,
                    CNPJ = "66907757000171",
                    Name = "Fernando",
                    Phone = "1231234654",
                    Email = "fernando@email.com",
                    AddressId = 2
                }
            );
        }
    }
}