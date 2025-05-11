using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class mode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mode",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mode",
                table: "AspNetUsers");
        }
    }
}
