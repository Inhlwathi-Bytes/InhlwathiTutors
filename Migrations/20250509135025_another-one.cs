using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class anotherone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorshipLanguages_Tutorships_TutorshipId1",
                table: "TutorshipLanguages");

            migrationBuilder.DropIndex(
                name: "IX_TutorshipLanguages_TutorshipId1",
                table: "TutorshipLanguages");

            migrationBuilder.DropColumn(
                name: "TutorshipId1",
                table: "TutorshipLanguages");

            migrationBuilder.AlterColumn<int>(
                name: "TutorshipId",
                table: "TutorshipLanguages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipLanguages_TutorshipId",
                table: "TutorshipLanguages",
                column: "TutorshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorshipLanguages_Tutorships_TutorshipId",
                table: "TutorshipLanguages",
                column: "TutorshipId",
                principalTable: "Tutorships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorshipLanguages_Tutorships_TutorshipId",
                table: "TutorshipLanguages");

            migrationBuilder.DropIndex(
                name: "IX_TutorshipLanguages_TutorshipId",
                table: "TutorshipLanguages");

            migrationBuilder.AlterColumn<string>(
                name: "TutorshipId",
                table: "TutorshipLanguages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TutorshipId1",
                table: "TutorshipLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipLanguages_TutorshipId1",
                table: "TutorshipLanguages",
                column: "TutorshipId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorshipLanguages_Tutorships_TutorshipId1",
                table: "TutorshipLanguages",
                column: "TutorshipId1",
                principalTable: "Tutorships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
