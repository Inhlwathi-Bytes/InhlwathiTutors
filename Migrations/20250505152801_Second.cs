using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InhlwathiTutors.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "AspNetUsers",
                newName: "Location");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tutorships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Qualifications = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Achievements = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Rating = table.Column<double>(type: "double", nullable: false),
                    ProfilePhotoPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutorships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutorships_AspNetUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TutorshipLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TutorshipId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TutorshipId1 = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorshipLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorshipLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorshipLanguages_Tutorships_TutorshipId1",
                        column: x => x.TutorshipId1,
                        principalTable: "Tutorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TutorshipSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubjectName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Availability = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Outline = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HourlyRate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Level = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IntroVideoLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TutorshipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorshipSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorshipSubjects_Tutorships_TutorshipId",
                        column: x => x.TutorshipId,
                        principalTable: "Tutorships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TutorshipSubjectLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TutorshipSubjectId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorshipSubjectLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorshipSubjectLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorshipSubjectLanguages_TutorshipSubjects_TutorshipSubject~",
                        column: x => x.TutorshipSubjectId,
                        principalTable: "TutorshipSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipLanguages_LanguageId",
                table: "TutorshipLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipLanguages_TutorshipId1",
                table: "TutorshipLanguages",
                column: "TutorshipId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorships_SystemUserId",
                table: "Tutorships",
                column: "SystemUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipSubjectLanguages_LanguageId",
                table: "TutorshipSubjectLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipSubjectLanguages_TutorshipSubjectId",
                table: "TutorshipSubjectLanguages",
                column: "TutorshipSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorshipSubjects_TutorshipId",
                table: "TutorshipSubjects",
                column: "TutorshipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorshipLanguages");

            migrationBuilder.DropTable(
                name: "TutorshipSubjectLanguages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "TutorshipSubjects");

            migrationBuilder.DropTable(
                name: "Tutorships");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "AspNetUsers",
                newName: "Institution");
        }
    }
}
