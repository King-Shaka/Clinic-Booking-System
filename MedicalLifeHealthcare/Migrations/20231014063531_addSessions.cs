using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class addSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Records_AspNetUsers_NurseID",
                table: "Medical_Records");

            migrationBuilder.AlterColumn<string>(
                name: "NurseID",
                table: "Medical_Records",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Counselling_Sessions",
                columns: table => new
                {
                    SessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CounsellorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointemtID = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counselling_Sessions", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Counselling_Sessions_Appointments_AppointemtID",
                        column: x => x.AppointemtID,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentID");
                    table.ForeignKey(
                        name: "FK_Counselling_Sessions_AspNetUsers_CounsellorID",
                        column: x => x.CounsellorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counselling_Sessions_AppointemtID",
                table: "Counselling_Sessions",
                column: "AppointemtID");

            migrationBuilder.CreateIndex(
                name: "IX_Counselling_Sessions_CounsellorID",
                table: "Counselling_Sessions",
                column: "CounsellorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Records_AspNetUsers_NurseID",
                table: "Medical_Records",
                column: "NurseID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Records_AspNetUsers_NurseID",
                table: "Medical_Records");

            migrationBuilder.DropTable(
                name: "Counselling_Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "NurseID",
                table: "Medical_Records",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Records_AspNetUsers_NurseID",
                table: "Medical_Records",
                column: "NurseID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
