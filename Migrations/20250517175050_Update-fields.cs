using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class Updatefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryMode",
                table: "TutorshipSubjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMode",
                table: "TutorshipSubjects");
        }
    }
}
