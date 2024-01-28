using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class samples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    SampleCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestRequestId = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleContainerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.SampleCollectionId);
                    table.ForeignKey(
                        name: "FK_Samples_TestRequest_TestRequestId",
                        column: x => x.TestRequestId,
                        principalTable: "TestRequest",
                        principalColumn: "TestRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samples_TestRequestId",
                table: "Samples",
                column: "TestRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");
        }
    }
}
