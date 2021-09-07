using Microsoft.EntityFrameworkCore.Migrations;

namespace Supermarket.API.Migrations
{
    public partial class productremoveunitprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInPackage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "QuantityInPackage",
                table: "Products",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<byte>(
                name: "UnitOfMeasurement",
                table: "Products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "QuantityInPackage", "UnitOfMeasurement" },
                values: new object[] { (short)1, (byte)1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "QuantityInPackage", "UnitOfMeasurement" },
                values: new object[] { (short)2, (byte)5 });
        }
    }
}
