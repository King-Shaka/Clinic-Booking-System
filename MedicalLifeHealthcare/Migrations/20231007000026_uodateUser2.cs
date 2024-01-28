using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class uodateUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medical_Records",
                columns: table => new
                {
                    RecordsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileID = table.Column<int>(type: "int", nullable: false),
                    CurrentMedications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodPressure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HeartRate = table.Column<int>(type: "int", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NurseID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Records", x => x.RecordsID);
                    table.ForeignKey(
                        name: "FK_Medical_Records_AspNetUsers_NurseID",
                        column: x => x.NurseID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medical_Records_Medical_File_FileID",
                        column: x => x.FileID,
                        principalTable: "Medical_File",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_FileID",
                table: "Medical_Records",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_NurseID",
                table: "Medical_Records",
                column: "NurseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medical_Records");
        }
    }
}
