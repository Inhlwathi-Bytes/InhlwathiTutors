using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class reviews2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReview_AspNetUsers_ReviewerId",
                table: "SubjectReview");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectReview_TutorshipSubjects_SubjectId1",
                table: "SubjectReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectReview",
                table: "SubjectReview");

            migrationBuilder.RenameTable(
                name: "SubjectReview",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectReview_SubjectId1",
                table: "Reviews",
                newName: "IX_Reviews_SubjectId1");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectReview_ReviewerId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TutorshipSubjects_SubjectId1",
                table: "Reviews",
                column: "SubjectId1",
                principalTable: "TutorshipSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TutorshipSubjects_SubjectId1",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "SubjectReview");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_SubjectId1",
                table: "SubjectReview",
                newName: "IX_SubjectReview_SubjectId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewerId",
                table: "SubjectReview",
                newName: "IX_SubjectReview_ReviewerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectReview",
                table: "SubjectReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReview_AspNetUsers_ReviewerId",
                table: "SubjectReview",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectReview_TutorshipSubjects_SubjectId1",
                table: "SubjectReview",
                column: "SubjectId1",
                principalTable: "TutorshipSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
