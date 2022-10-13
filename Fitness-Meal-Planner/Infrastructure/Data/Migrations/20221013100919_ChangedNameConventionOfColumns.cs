using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedNameConventionOfColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastModifiedBy",
                table: "Users",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Users",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "emailAddress",
                table: "Users",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Users",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "weightInGrams",
                table: "Products",
                newName: "WeightInGrams");

            migrationBuilder.RenameColumn(
                name: "protein",
                table: "Products",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "productPhotoPath",
                table: "Products",
                newName: "ProductPhotoPath");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lastModifiedBy",
                table: "Products",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Products",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "ingredients",
                table: "Products",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "fat",
                table: "Products",
                newName: "Fat");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Products",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Products",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "carbohydrates",
                table: "Products",
                newName: "Carbohydrates");

            migrationBuilder.RenameColumn(
                name: "calories",
                table: "Products",
                newName: "Calories");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "weightInGrams",
                table: "Meals",
                newName: "WeightInGrams");

            migrationBuilder.RenameColumn(
                name: "recipe",
                table: "Meals",
                newName: "Recipe");

            migrationBuilder.RenameColumn(
                name: "protein",
                table: "Meals",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Meals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "mealPhotoPath",
                table: "Meals",
                newName: "MealPhotoPath");

            migrationBuilder.RenameColumn(
                name: "lastModifiedBy",
                table: "Meals",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Meals",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "ingredients",
                table: "Meals",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "fat",
                table: "Meals",
                newName: "Fat");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Meals",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Meals",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "carbohydrates",
                table: "Meals",
                newName: "Carbohydrates");

            migrationBuilder.RenameColumn(
                name: "calories",
                table: "Meals",
                newName: "Calories");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Meals",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Users",
                newName: "lastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Users",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Users",
                newName: "emailAddress");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Users",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "WeightInGrams",
                table: "Products",
                newName: "weightInGrams");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Products",
                newName: "protein");

            migrationBuilder.RenameColumn(
                name: "ProductPhotoPath",
                table: "Products",
                newName: "productPhotoPath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Products",
                newName: "lastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Products",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Products",
                newName: "ingredients");

            migrationBuilder.RenameColumn(
                name: "Fat",
                table: "Products",
                newName: "fat");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Products",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Products",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Carbohydrates",
                table: "Products",
                newName: "carbohydrates");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Products",
                newName: "calories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WeightInGrams",
                table: "Meals",
                newName: "weightInGrams");

            migrationBuilder.RenameColumn(
                name: "Recipe",
                table: "Meals",
                newName: "recipe");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Meals",
                newName: "protein");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Meals",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MealPhotoPath",
                table: "Meals",
                newName: "mealPhotoPath");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Meals",
                newName: "lastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Meals",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Meals",
                newName: "ingredients");

            migrationBuilder.RenameColumn(
                name: "Fat",
                table: "Meals",
                newName: "fat");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Meals",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Meals",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Carbohydrates",
                table: "Meals",
                newName: "carbohydrates");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Meals",
                newName: "calories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Meals",
                newName: "id");
        }
    }
}
