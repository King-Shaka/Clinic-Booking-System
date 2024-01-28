using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class addprescritio2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medical_Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescresptionID = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorsFeedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnsweredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DoctorsID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Feedback", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Medical_Feedback_AspNetUsers_DoctorsID",
                        column: x => x.DoctorsID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medical_Feedback_AspNetUsers_PatientID",
                        column: x => x.PatientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medical_Feedback_Prescription_PrescresptionID",
                        column: x => x.PrescresptionID,
                        principalTable: "Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Feedback_DoctorsID",
                table: "Medical_Feedback",
                column: "DoctorsID");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Feedback_PatientID",
                table: "Medical_Feedback",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Feedback_PrescresptionID",
                table: "Medical_Feedback",
                column: "PrescresptionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medical_Feedback");
        }
    }
}
