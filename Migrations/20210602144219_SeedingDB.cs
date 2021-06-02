using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioAPI.Migrations
{
    public partial class SeedingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClientAddresses",
                columns: new[] { "Id", "CEP", "City", "Complement", "District", "Number", "State", "Street" },
                values: new object[,]
                {
                    { 1, "423921837", "Sorocaba", "", "Centro", 233, "SP", "Rua Osvaldo da Cruz" },
                    { 2, "123463456", "Curitiba", "Apt 302", "Vila Aviação", 1245, "PR", "Av Nações Unidas" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Unit" },
                values: new object[,]
                {
                    { 1, "Caneca do Ben10", "Caneca", "Unidade" },
                    { 2, "Banana nanica", "Banana", "Kg" },
                    { 3, "Vela cheirosa", "Vela", "Unidade" }
                });

            migrationBuilder.InsertData(
                table: "SupplierAddresses",
                columns: new[] { "Id", "CEP", "City", "Complement", "District", "Number", "State", "Street" },
                values: new object[,]
                {
                    { 1, "523423412", "Bauru", "", "Centro", 111, "SP", "Rua Faria Lima" },
                    { 2, "94656345", "São Paulo", "Apt 51", "Rebouças", 347, "SP", "Av Castelo Branco" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "CPF", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "d3879743-3fa8-4a7d-a9e6-90b83c0accd0", 0, 1, "80048589020", "700e029e-b19b-42de-8ac0-7a1f300be604", "adm@admin.com", false, true, null, "ADM", "ADM@ADMIN.COM", "ADM@ADMIN.COM", "AQAAAAEAACcQAAAAELAa1eh4XGDLWkrc0KDHXsPlAwIRKw1s5xo0Wlpxzx3O/4q/AwltH7GPdY7XOekI7w==", "423423412", false, "JCNS4XN7FOFOWL5FS6LJRKMBN2CQ3GM3", false, "adm@admin.com" },
                    { "929fd92e-1292-4072-b705-5d9593209d99", 0, 2, "20098038095", "a8d3e6e2-b9d0-4131-9507-3f5966c0c931", "joao@email.com", false, true, null, "Joao", "JOAO@EMAIL.COM", "JOAO@EMAIL.COM", "AQAAAAEAACcQAAAAENFgRhnuzKBL7PW+BAvODX9jcwy3VTM5d6ldcSl1lFMSlUxuH8jAeJTe4CXzaiCrRA==", "123987767", false, "G7J67KY6N4P5W4XKIDBIW3U5LEPPB6EJ", false, "joao@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "AddressId", "CNPJ", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "06129000000172", "joaquim@email.com", "Joaquim", "654645612" },
                    { 2, 2, "66907757000171", "fernando@email.com", "Fernando", "1231234654" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "929fd92e-1292-4072-b705-5d9593209d99");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3879743-3fa8-4a7d-a9e6-90b83c0accd0");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClientAddresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientAddresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SupplierAddresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SupplierAddresses",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
