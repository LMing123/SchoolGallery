using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolGallery.Migrations
{
    public partial class add_sort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Content",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Category",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Category");
        }
    }
}
