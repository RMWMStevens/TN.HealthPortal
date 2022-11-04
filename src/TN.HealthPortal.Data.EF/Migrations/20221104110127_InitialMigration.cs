using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TN.HealthPortal.Data.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Pathogens",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pathogens", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gilt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarians",
                columns: table => new
                {
                    EmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.EmployeeCode);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerName",
                        column: x => x.ManufacturerName,
                        principalTable: "Manufacturers",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    BlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PremiseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => new { x.BlnNumber, x.PremiseID });
                    table.ForeignKey(
                        name: "FK_Farms_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionPhase = table.Column<int>(type: "int", nullable: false),
                    PigCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schemes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Disease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmEntityBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FarmEntityPremiseID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiseaseStatuses_Farms_FarmEntityBlnNumber_FarmEntityPremiseID",
                        columns: x => new { x.FarmEntityBlnNumber, x.FarmEntityPremiseID },
                        principalTable: "Farms",
                        principalColumns: new[] { "BlnNumber", "PremiseID" });
                });

            migrationBuilder.CreateTable(
                name: "FarmVeterinarians",
                columns: table => new
                {
                    VeterinariansEmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmsBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmsPremiseID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmVeterinarians", x => new { x.VeterinariansEmployeeCode, x.FarmsBlnNumber, x.FarmsPremiseID });
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Farms_FarmsBlnNumber_FarmsPremiseID",
                        columns: x => new { x.FarmsBlnNumber, x.FarmsPremiseID },
                        principalTable: "Farms",
                        principalColumns: new[] { "BlnNumber", "PremiseID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Veterinarians_VeterinariansEmployeeCode",
                        column: x => x.VeterinariansEmployeeCode,
                        principalTable: "Veterinarians",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DewormingSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteOfAdministration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmEntityBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FarmEntityPremiseID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DewormingSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DewormingSchemes_Farms_FarmEntityBlnNumber_FarmEntityPremiseID",
                        columns: x => new { x.FarmEntityBlnNumber, x.FarmEntityPremiseID },
                        principalTable: "Farms",
                        principalColumns: new[] { "BlnNumber", "PremiseID" });
                    table.ForeignKey(
                        name: "FK_DewormingSchemes_Schemes_Id",
                        column: x => x.Id,
                        principalTable: "Schemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VaccinationSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PathogenName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmEntityBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FarmEntityPremiseID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Farms_FarmEntityBlnNumber_FarmEntityPremiseID",
                        columns: x => new { x.FarmEntityBlnNumber, x.FarmEntityPremiseID },
                        principalTable: "Farms",
                        principalColumns: new[] { "BlnNumber", "PremiseID" });
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Pathogens_PathogenName",
                        column: x => x.PathogenName,
                        principalTable: "Pathogens",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Schemes_Id",
                        column: x => x.Id,
                        principalTable: "Schemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DewormingSchemes_FarmEntityBlnNumber_FarmEntityPremiseID",
                table: "DewormingSchemes",
                columns: new[] { "FarmEntityBlnNumber", "FarmEntityPremiseID" });

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseStatuses_FarmEntityBlnNumber_FarmEntityPremiseID",
                table: "DiseaseStatuses",
                columns: new[] { "FarmEntityBlnNumber", "FarmEntityPremiseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Farms_SourceId",
                table: "Farms",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmVeterinarians_FarmsBlnNumber_FarmsPremiseID",
                table: "FarmVeterinarians",
                columns: new[] { "FarmsBlnNumber", "FarmsPremiseID" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerName",
                table: "Products",
                column: "ManufacturerName");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_ProductId",
                table: "Schemes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_FarmEntityBlnNumber_FarmEntityPremiseID",
                table: "VaccinationSchemes",
                columns: new[] { "FarmEntityBlnNumber", "FarmEntityPremiseID" });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_PathogenName",
                table: "VaccinationSchemes",
                column: "PathogenName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DewormingSchemes");

            migrationBuilder.DropTable(
                name: "DiseaseStatuses");

            migrationBuilder.DropTable(
                name: "FarmVeterinarians");

            migrationBuilder.DropTable(
                name: "VaccinationSchemes");

            migrationBuilder.DropTable(
                name: "Veterinarians");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Pathogens");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
