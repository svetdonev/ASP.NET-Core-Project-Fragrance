using Microsoft.EntityFrameworkCore.Migrations;

namespace FragranceProject.Data.Migrations
{
    public partial class RemoveSortingFromFragranceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sorting",
                table: "Fragrances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sorting",
                table: "Fragrances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
