using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TN.HealthPortal.Data.EF.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DewormingSchemes_Farms_FarmBlnNumber",
                table: "DewormingSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationSchemes_Farms_FarmBlnNumber",
                table: "VaccinationSchemes");

            migrationBuilder.AlterColumn<string>(
                name: "FarmBlnNumber",
                table: "VaccinationSchemes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FarmBlnNumber",
                table: "DewormingSchemes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DewormingSchemes_Farms_FarmBlnNumber",
                table: "DewormingSchemes",
                column: "FarmBlnNumber",
                principalTable: "Farms",
                principalColumn: "BlnNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationSchemes_Farms_FarmBlnNumber",
                table: "VaccinationSchemes",
                column: "FarmBlnNumber",
                principalTable: "Farms",
                principalColumn: "BlnNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DewormingSchemes_Farms_FarmBlnNumber",
                table: "DewormingSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationSchemes_Farms_FarmBlnNumber",
                table: "VaccinationSchemes");

            migrationBuilder.AlterColumn<string>(
                name: "FarmBlnNumber",
                table: "VaccinationSchemes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FarmBlnNumber",
                table: "DewormingSchemes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DewormingSchemes_Farms_FarmBlnNumber",
                table: "DewormingSchemes",
                column: "FarmBlnNumber",
                principalTable: "Farms",
                principalColumn: "BlnNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationSchemes_Farms_FarmBlnNumber",
                table: "VaccinationSchemes",
                column: "FarmBlnNumber",
                principalTable: "Farms",
                principalColumn: "BlnNumber");
        }
    }
}
