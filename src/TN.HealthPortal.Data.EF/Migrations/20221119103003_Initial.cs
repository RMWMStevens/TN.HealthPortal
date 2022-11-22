using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TN.HealthPortal.Data.EF.Migrations
{
    public partial class Initial : Migration
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
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pathogens", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarians",
                columns: table => new
                {
                    EmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.EmployeeCode);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerName",
                        column: x => x.ManufacturerName,
                        principalTable: "Manufacturers",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionName",
                        column: x => x.RegionName,
                        principalTable: "Regions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionVeterinarians",
                columns: table => new
                {
                    RegionsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VeterinariansEmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionVeterinarians", x => new { x.RegionsName, x.VeterinariansEmployeeCode });
                    table.ForeignKey(
                        name: "FK_RegionVeterinarians_Regions_RegionsName",
                        column: x => x.RegionsName,
                        principalTable: "Regions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionVeterinarians_Veterinarians_VeterinariansEmployeeCode",
                        column: x => x.VeterinariansEmployeeCode,
                        principalTable: "Veterinarians",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryVeterinarians",
                columns: table => new
                {
                    CountriesName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VeterinariansEmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryVeterinarians", x => new { x.CountriesName, x.VeterinariansEmployeeCode });
                    table.ForeignKey(
                        name: "FK_CountryVeterinarians_Countries_CountriesName",
                        column: x => x.CountriesName,
                        principalTable: "Countries",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryVeterinarians_Veterinarians_VeterinariansEmployeeCode",
                        column: x => x.VeterinariansEmployeeCode,
                        principalTable: "Veterinarians",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    BlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiseId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManuallyUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.BlnNumber);
                    table.ForeignKey(
                        name: "FK_Farms_Countries_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Countries",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.FarmBlnNumber);
                    table.ForeignKey(
                        name: "FK_Addresses_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DewormingSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionPhase = table.Column<int>(type: "int", nullable: false),
                    PigCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RouteOfAdministration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DewormingSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DewormingSchemes_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber");
                    table.ForeignKey(
                        name: "FK_DewormingSchemes_Products_ProductName",
                        column: x => x.ProductName,
                        principalTable: "Products",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseStatuses",
                columns: table => new
                {
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Disease = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseStatuses", x => new { x.FarmBlnNumber, x.Disease });
                    table.ForeignKey(
                        name: "FK_DiseaseStatuses_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmVeterinarians",
                columns: table => new
                {
                    FarmsBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VeterinariansEmployeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmVeterinarians", x => new { x.FarmsBlnNumber, x.VeterinariansEmployeeCode });
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Farms_FarmsBlnNumber",
                        column: x => x.FarmsBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmVeterinarians_Veterinarians_VeterinariansEmployeeCode",
                        column: x => x.VeterinariansEmployeeCode,
                        principalTable: "Veterinarians",
                        principalColumn: "EmployeeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionTypes", x => new { x.Name, x.FarmBlnNumber });
                    table.ForeignKey(
                        name: "FK_ProductionTypes_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => new { x.FarmBlnNumber, x.Category });
                    table.ForeignKey(
                        name: "FK_Sources_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccinationSchemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PathogenName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmBlnNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByEmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionPhase = table.Column<int>(type: "int", nullable: false),
                    PigCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RouteOfAdministration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationSchemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Farms_FarmBlnNumber",
                        column: x => x.FarmBlnNumber,
                        principalTable: "Farms",
                        principalColumn: "BlnNumber");
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Pathogens_PathogenName",
                        column: x => x.PathogenName,
                        principalTable: "Pathogens",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccinationSchemes_Products_ProductName",
                        column: x => x.ProductName,
                        principalTable: "Products",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionName",
                table: "Countries",
                column: "RegionName");

            migrationBuilder.CreateIndex(
                name: "IX_CountryVeterinarians_VeterinariansEmployeeCode",
                table: "CountryVeterinarians",
                column: "VeterinariansEmployeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_DewormingSchemes_FarmBlnNumber",
                table: "DewormingSchemes",
                column: "FarmBlnNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DewormingSchemes_ProductName",
                table: "DewormingSchemes",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_CountryName",
                table: "Farms",
                column: "CountryName");

            migrationBuilder.CreateIndex(
                name: "IX_FarmVeterinarians_VeterinariansEmployeeCode",
                table: "FarmVeterinarians",
                column: "VeterinariansEmployeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionTypes_FarmBlnNumber",
                table: "ProductionTypes",
                column: "FarmBlnNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerName",
                table: "Products",
                column: "ManufacturerName");

            migrationBuilder.CreateIndex(
                name: "IX_RegionVeterinarians_VeterinariansEmployeeCode",
                table: "RegionVeterinarians",
                column: "VeterinariansEmployeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_FarmBlnNumber",
                table: "VaccinationSchemes",
                column: "FarmBlnNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_PathogenName",
                table: "VaccinationSchemes",
                column: "PathogenName");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchemes_ProductName",
                table: "VaccinationSchemes",
                column: "ProductName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CountryVeterinarians");

            migrationBuilder.DropTable(
                name: "DewormingSchemes");

            migrationBuilder.DropTable(
                name: "DiseaseStatuses");

            migrationBuilder.DropTable(
                name: "FarmVeterinarians");

            migrationBuilder.DropTable(
                name: "ProductionTypes");

            migrationBuilder.DropTable(
                name: "RegionVeterinarians");

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
                name: "Products");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
