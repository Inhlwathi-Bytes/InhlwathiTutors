using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class updateAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Tutorships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Tutorships");
        }
    }
}
