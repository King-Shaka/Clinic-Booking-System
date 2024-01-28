using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Session_Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionID = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counsellor_Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Satus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session_Feedback", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Session_Feedback_Counselling_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Counselling_Sessions",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_Feedback_SessionID",
                table: "Session_Feedback",
                column: "SessionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session_Feedback");
        }
    }
}
