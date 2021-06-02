using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAPI.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product {
                    Id = 1,
                    Name = "Caneca",
                    Description = "Caneca do Ben10",
                    Unit = "Unidade"
                },
                new Product {
                    Id = 2,
                    Name = "Banana",
                    Description = "Banana nanica",
                    Unit = "Kg"
                },
                new Product {
                    Id = 3,
                    Name = "Vela",
                    Description = "Vela cheirosa",
                    Unit = "Unidade"
                }
            );
        }
    }
}