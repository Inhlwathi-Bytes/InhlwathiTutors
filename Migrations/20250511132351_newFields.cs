using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class newFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qualifications",
                table: "Tutorships",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Achievements",
                table: "Tutorships",
                newName: "Province");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Tutorships",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HighestAchievement",
                table: "Tutorships",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Tutorships",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Tutorships");

            migrationBuilder.DropColumn(
                name: "HighestAchievement",
                table: "Tutorships");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Tutorships");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Tutorships",
                newName: "Qualifications");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Tutorships",
                newName: "Achievements");
        }
    }
}
