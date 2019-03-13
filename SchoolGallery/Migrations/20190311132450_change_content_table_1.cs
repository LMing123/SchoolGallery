using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolGallery.Migrations
{
    public partial class change_content_table_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVideo",
                table: "Content",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVideo",
                table: "Content");
        }
    }
}
