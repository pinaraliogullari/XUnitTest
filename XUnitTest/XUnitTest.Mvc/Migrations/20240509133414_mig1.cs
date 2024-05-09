using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XUnitTest.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Color", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("13d11f29-8e5e-43e8-82ce-8f8c0fa2a4a3"), "Green", "Product 4", 5000m, 250 },
                    { new Guid("56aeb717-6761-4d10-be09-88c43a1f2d6b"), "Yellow", "Product 3", 4000m, 200 },
                    { new Guid("a6749f60-4616-427b-980b-51545683cda0"), "Red", "Product 1", 2000m, 100 },
                    { new Guid("b02c1fb4-361f-42c4-a868-0c1b5b9f9a69"), "Black", "Product 2", 3000m, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
