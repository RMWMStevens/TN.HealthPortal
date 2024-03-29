﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TN.HealthPortal.Data.EF;

#nullable disable

namespace TN.HealthPortal.Data.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CountryVeterinarian", b =>
                {
                    b.Property<string>("CountriesName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VeterinariansEmployeeCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CountriesName", "VeterinariansEmployeeCode");

                    b.HasIndex("VeterinariansEmployeeCode");

                    b.ToTable("CountryVeterinarians", (string)null);
                });

            modelBuilder.Entity("FarmVeterinarian", b =>
                {
                    b.Property<string>("FarmsBlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VeterinariansEmployeeCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FarmsBlnNumber", "VeterinariansEmployeeCode");

                    b.HasIndex("VeterinariansEmployeeCode");

                    b.ToTable("FarmVeterinarians", (string)null);
                });

            modelBuilder.Entity("RegionVeterinarian", b =>
                {
                    b.Property<string>("RegionsName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VeterinariansEmployeeCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RegionsName", "VeterinariansEmployeeCode");

                    b.HasIndex("VeterinariansEmployeeCode");

                    b.ToTable("RegionVeterinarians", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Address", b =>
                {
                    b.Property<string>("FarmBlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FarmBlnNumber");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Country", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("RegionName");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.DewormingScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FarmBlnNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PigCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductionPhase")
                        .HasColumnType("int");

                    b.Property<int>("RouteOfAdministration")
                        .HasColumnType("int");

                    b.Property<string>("Timing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FarmBlnNumber");

                    b.HasIndex("ProductName");

                    b.ToTable("DewormingSchemes", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.DiseaseStatus", b =>
                {
                    b.Property<string>("FarmBlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Disease")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FarmBlnNumber", "Disease");

                    b.ToTable("DiseaseStatuses", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Farm", b =>
                {
                    b.Property<string>("BlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ManuallyUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PremiseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlnNumber");

                    b.HasIndex("CountryName");

                    b.ToTable("Farms", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Manufacturer", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Manufacturers", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Pathogen", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Pathogens", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Product", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.HasIndex("ManufacturerName");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.ProductionType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FarmBlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name", "FarmBlnNumber");

                    b.HasIndex("FarmBlnNumber");

                    b.ToTable("ProductionTypes", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Region", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Regions", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Source", b =>
                {
                    b.Property<string>("FarmBlnNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FarmBlnNumber", "Category");

                    b.ToTable("Sources", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.VaccinationScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FarmBlnNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PathogenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PigCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductionPhase")
                        .HasColumnType("int");

                    b.Property<int>("RouteOfAdministration")
                        .HasColumnType("int");

                    b.Property<string>("Timing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FarmBlnNumber");

                    b.HasIndex("PathogenName");

                    b.HasIndex("ProductName");

                    b.ToTable("VaccinationSchemes", (string)null);
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Veterinarian", b =>
                {
                    b.Property<string>("EmployeeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeCode");

                    b.ToTable("Veterinarians", (string)null);
                });

            modelBuilder.Entity("CountryVeterinarian", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Veterinarian", null)
                        .WithMany()
                        .HasForeignKey("VeterinariansEmployeeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmVeterinarian", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany()
                        .HasForeignKey("FarmsBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Veterinarian", null)
                        .WithMany()
                        .HasForeignKey("VeterinariansEmployeeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegionVeterinarian", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Region", null)
                        .WithMany()
                        .HasForeignKey("RegionsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Veterinarian", null)
                        .WithMany()
                        .HasForeignKey("VeterinariansEmployeeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Address", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithOne("Address")
                        .HasForeignKey("TN.HealthPortal.Logic.Entities.Address", "FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Country", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.DewormingScheme", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany("DewormingSchemes")
                        .HasForeignKey("FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.DiseaseStatus", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany("DiseaseStatuses")
                        .HasForeignKey("FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Farm", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Product", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.ProductionType", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany("ProductionTypes")
                        .HasForeignKey("FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Source", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany("Sources")
                        .HasForeignKey("FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.VaccinationScheme", b =>
                {
                    b.HasOne("TN.HealthPortal.Logic.Entities.Farm", null)
                        .WithMany("VaccinationSchemes")
                        .HasForeignKey("FarmBlnNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Pathogen", "Pathogen")
                        .WithMany()
                        .HasForeignKey("PathogenName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TN.HealthPortal.Logic.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pathogen");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Farm", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("DewormingSchemes");

                    b.Navigation("DiseaseStatuses");

                    b.Navigation("ProductionTypes");

                    b.Navigation("Sources");

                    b.Navigation("VaccinationSchemes");
                });

            modelBuilder.Entity("TN.HealthPortal.Logic.Entities.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
