using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CollectorName",
                table: "Samples",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SampleResults",
                columns: table => new
                {
                    SampleResultsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SamplesID = table.Column<int>(type: "int", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interpretation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathologyID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleResults", x => x.SampleResultsId);
                    table.ForeignKey(
                        name: "FK_SampleResults_AspNetUsers_PathologyID",
                        column: x => x.PathologyID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SampleResults_Samples_SamplesID",
                        column: x => x.SamplesID,
                        principalTable: "Samples",
                        principalColumn: "SampleCollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samples_CollectorName",
                table: "Samples",
                column: "CollectorName");

            migrationBuilder.CreateIndex(
                name: "IX_SampleResults_PathologyID",
                table: "SampleResults",
                column: "PathologyID");

            migrationBuilder.CreateIndex(
                name: "IX_SampleResults_SamplesID",
                table: "SampleResults",
                column: "SamplesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_AspNetUsers_CollectorName",
                table: "Samples",
                column: "CollectorName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samples_AspNetUsers_CollectorName",
                table: "Samples");

            migrationBuilder.DropTable(
                name: "SampleResults");

            migrationBuilder.DropIndex(
                name: "IX_Samples_CollectorName",
                table: "Samples");

            migrationBuilder.AlterColumn<string>(
                name: "CollectorName",
                table: "Samples",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
