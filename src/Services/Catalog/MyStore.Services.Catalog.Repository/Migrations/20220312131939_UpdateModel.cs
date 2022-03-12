using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStore.Services.Catalog.Repository.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SmallDescription",
                table: "Brands",
                newName: "ShortDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Brands",
                newName: "SmallDescription");
        }
    }
}
