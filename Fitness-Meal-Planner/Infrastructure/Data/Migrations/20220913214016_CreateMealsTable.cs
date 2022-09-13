using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CreateMealsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    weightInGrams = table.Column<int>(type: "int", nullable: false),
                    calories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    carbohydrates = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ingredients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    recipe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
