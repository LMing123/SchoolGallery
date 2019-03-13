using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolGallery.Migrations
{
    public partial class change_content_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModifiedIP",
                table: "Content",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishTime",
                table: "Content",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PublisherID",
                table: "Content",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedIP",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "PublishTime",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Content");
        }
    }
}
