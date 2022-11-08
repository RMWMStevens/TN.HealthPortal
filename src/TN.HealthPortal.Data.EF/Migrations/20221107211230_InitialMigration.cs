using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TN.HealthPortal.Data.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlnNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiseID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

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
                name: "Veterinarians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Disease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiseaseStatuses_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    FarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gilt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.FarmId);
                    table.ForeignKey(
                        name: "FK_Sources_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "FarmVeterinarians",
                columns: table => new
                {
                    FarmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeterinariansId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmVeterinarians", x => new { x.FarmsId, x.VeterinariansId });
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Farms_FarmsId",
                        column: x => x.FarmsId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Veterinarians_VeterinariansId",
                        column: x => x.VeterinariansId,
                        principalTable: "Veterinarians",
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
                name: "DewormingSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteOfAdministration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DewormingSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DewormingSchemes_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
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
                    FarmId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
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
                name: "IX_DewormingSchemes_FarmId",
                table: "DewormingSchemes",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseStatuses_FarmId",
                table: "DiseaseStatuses",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmVeterinarians_VeterinariansId",
                table: "FarmVeterinarians",
                column: "VeterinariansId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerName",
                table: "Products",
                column: "ManufacturerName");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_ProductId",
                table: "Schemes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_FarmId",
                table: "VaccinationSchemes",
                column: "FarmId");

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
                name: "Sources");

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
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
