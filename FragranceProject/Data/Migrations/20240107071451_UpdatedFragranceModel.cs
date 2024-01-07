using Microsoft.EntityFrameworkCore.Migrations;

namespace FragranceProject.Data.Migrations
{
    public partial class UpdatedFragranceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Milliliters",
                table: "Fragrances",
                newName: "Sorting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sorting",
                table: "Fragrances",
                newName: "Milliliters");
        }
    }
}
