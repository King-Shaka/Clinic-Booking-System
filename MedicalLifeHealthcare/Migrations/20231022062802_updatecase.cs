using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalLifeHealthcare.Migrations
{
    public partial class updatecase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "IncidentReport",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "IncidentReport");
        }
    }
}
