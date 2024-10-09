using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ProductName", "UnitPrice", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 3, "Apple iPhone 14", 999.99m, 100 },
                    { 2, 3, "Samsung Galaxy S22", 799.99m, 150 },
                    { 3, 5, "Sony WH-1000XM5 Headphones", 299.99m, 200 },
                    { 4, 4, "Dell XPS 13 Laptop", 1500.00m, 80 },
                    { 5, 5, "Apple MacBook Pro", 2300.00m, 50 },
                    { 6, 7, "Amazon Echo Dot", 49.99m, 300 },
                    { 7, 6, "Bose SoundLink Speaker", 199.99m, 120 },
                    { 8, 2, "Google Pixel 7", 899.99m, 90 },
                    { 9, 1, "HP Envy 15 Laptop", 1399.99m, 60 },
                    { 10, 2, "Lenovo ThinkPad X1 Carbon", 1799.99m, 70 },
                    { 11, 4, "Apple iPad Pro", 1099.99m, 130 },
                    { 12, 3, "Microsoft Surface Laptop 4", 1199.99m, 110 },
                    { 13, 6, "JBL Charge 5 Speaker", 149.99m, 240 },
                    { 14, 5, "OnePlus 9 Pro", 799.99m, 90 },
                    { 15, 7, "Asus ROG Zephyrus Gaming Laptop", 2199.99m, 30 },
                    { 16, 1, "Fitbit Versa 3", 229.99m, 400 },
                    { 17, 8, "Sony PlayStation 5", 499.99m, 10 },
                    { 18, 7, "Apple AirPods Pro", 249.99m, 350 },
                    { 19, 6, "GoPro HERO10", 399.99m, 90 },
                    { 20, 8, "Nikon D7500 Camera", 1199.99m, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20);
        }
    }
}
