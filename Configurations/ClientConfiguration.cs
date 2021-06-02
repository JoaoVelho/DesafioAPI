using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAPI.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(
                new Client {
                    Id = "d3879743-3fa8-4a7d-a9e6-90b83c0accd0",
                    Name = "ADM",
                    UserName = "adm@admin.com",
                    NormalizedUserName = "ADM@ADMIN.COM",
                    Email = "adm@admin.com",
                    NormalizedEmail = "ADM@ADMIN.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAELAa1eh4XGDLWkrc0KDHXsPlAwIRKw1s5xo0Wlpxzx3O/4q/AwltH7GPdY7XOekI7w==",
                    SecurityStamp = "JCNS4XN7FOFOWL5FS6LJRKMBN2CQ3GM3",
                    ConcurrencyStamp = "700e029e-b19b-42de-8ac0-7a1f300be604",
                    PhoneNumber = "423423412",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    CPF = "80048589020",
                    AddressId = 1
                },
                new Client {
                    Id = "929fd92e-1292-4072-b705-5d9593209d99",
                    Name = "Joao",
                    UserName = "joao@email.com",
                    NormalizedUserName = "JOAO@EMAIL.COM",
                    Email = "joao@email.com",
                    NormalizedEmail = "JOAO@EMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAENFgRhnuzKBL7PW+BAvODX9jcwy3VTM5d6ldcSl1lFMSlUxuH8jAeJTe4CXzaiCrRA==",
                    SecurityStamp = "G7J67KY6N4P5W4XKIDBIW3U5LEPPB6EJ",
                    ConcurrencyStamp = "a8d3e6e2-b9d0-4131-9507-3f5966c0c931",
                    PhoneNumber = "123987767",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    CPF = "20098038095",
                    AddressId = 2
                }
            );
        }
    }
}