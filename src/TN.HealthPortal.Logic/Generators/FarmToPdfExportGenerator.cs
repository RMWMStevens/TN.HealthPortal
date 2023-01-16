using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Generators
{
    public class FarmToPdfExportGenerator : IFarmExportGenerator
    {
        public byte[] Generate(Farm farm)
        {
            using var ms = new MemoryStream();
            var document = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(25);
                    page.Content()
                        .DefaultTextStyle(style => style.FontSize(10))
                        .Element(container =>
                            container.PaddingVertical(10).Column(column =>
                            {
                                column.Spacing(5);
                                AddFarmTitleText(column, farm);
                                AddFarmInformationColumns(column, farm);
                                AddHealthStatusColumns(column, farm);
                                AddProtocolColumns(column, farm);
                            }));
                    page.Footer().AlignCenter().Text(_ =>
                    {
                        _.CurrentPageNumber();
                        _.Span(" / ");
                        _.TotalPages();
                    });
                });
            });
            document.GeneratePdf(ms);
            return ms.ToArray();
        }

        private static void AddFarmTitleText(ColumnDescriptor column, Farm farm)
        {
            column.Item().Text(text =>
            {
                text.AlignCenter();
                text.Span(farm.Name);
                text.DefaultTextStyle(style => style.FontSize(28));
            });
        }

        private void AddFarmInformationColumns(ColumnDescriptor column, Farm farm)
        {
            column.Item().Element(tableContainer =>
            {
                tableContainer
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn();
                        });

                        table.Cell().Element(HeaderCellStyle).Text("Name");
                        table.Cell().Element(CellStyle).Text(farm.Name);

                        table.Cell().Element(HeaderCellStyle).Text("BLN number");
                        table.Cell().Element(CellStyle).Text(farm.BlnNumber);

                        table.Cell().Element(HeaderCellStyle).Text("Location");
                        table.Cell().Element(CellStyle).Text($"{farm.Address.City}, {farm.Address.State}, {farm.Country.Name}");

                        table.Cell().Element(HeaderCellStyle).Text("Capacity");
                        table.Cell().Element(CellStyle).Text(farm.Capacity.ToString());

                        table.Cell().Element(HeaderCellStyle).Text("History");
                        table.Cell().Element(CellStyle).Text(farm.History);
                    });
            });

            column.Item().Text("Sources").FontSize(14);

            column.Item().Element(tableContainer =>
            {
                tableContainer
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn();
                        });

                        foreach (var source in farm.Sources)
                        {
                            table.Cell().Element(HeaderCellStyle).Text(source.Category);
                            table.Cell().Element(CellStyle).Text(source.Description);
                        }
                    });
            });
        }

        private void AddHealthStatusColumns(ColumnDescriptor column, Farm farm)
        {
            column.Item().Text("Health Status").FontSize(14);

            column.Item().Element(tableContainer =>
            {
                tableContainer
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(100);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Disease");
                            header.Cell().Element(HeaderCellStyle).Text("Status");
                        });

                        foreach (var diseaseStatus in farm.DiseaseStatuses)
                        {
                            table.Cell().Element(CellStyle).Text(diseaseStatus.Disease);
                            table.Cell().Element(CellStyle).Text(diseaseStatus.Status);
                        }
                    });
            });
        }

        private void AddProtocolColumns(ColumnDescriptor column, Farm farm)
        {
            column.Item().Text("Vaccination Protocol").FontSize(14);

            column.Item().Element(tableContainer =>
            {
                tableContainer
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Production Phase");
                            header.Cell().Element(HeaderCellStyle).Text("Disease Agent");
                            header.Cell().Element(HeaderCellStyle).Text("Manufacturer");
                            header.Cell().Element(HeaderCellStyle).Text("Product");
                            header.Cell().Element(HeaderCellStyle).Text("Dose");
                            header.Cell().Element(HeaderCellStyle).Text("Timing");
                        });

                        foreach (var scheme in farm.VaccinationSchemes)
                        {
                            table.Cell().Element(CellStyle).Text(scheme.ProductionPhase.ToString());
                            table.Cell().Element(CellStyle).Text(scheme.Pathogen.Name);
                            table.Cell().Element(CellStyle).Text(scheme.Product.Manufacturer.Name);
                            table.Cell().Element(CellStyle).Text(scheme.Product.Name);
                            table.Cell().Element(CellStyle).Text(scheme.Dose);
                            table.Cell().Element(CellStyle).Text(scheme.Timing);
                        }
                    });
            });

            column.Item().Text("Deworming Protocol").FontSize(14);

            column.Item().Element(tableContainer =>
            {
                tableContainer
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Production Phase");
                            header.Cell().Element(HeaderCellStyle).Text("Manufacturer");
                            header.Cell().Element(HeaderCellStyle).Text("Product");
                            header.Cell().Element(HeaderCellStyle).Text("Route of Administration");
                            header.Cell().Element(HeaderCellStyle).Text("Dose");
                            header.Cell().Element(HeaderCellStyle).Text("Timing");
                        });

                        foreach (var scheme in farm.DewormingSchemes)
                        {
                            table.Cell().Element(CellStyle).Text(scheme.ProductionPhase.ToString());
                            table.Cell().Element(CellStyle).Text(scheme.Product.Manufacturer.Name);
                            table.Cell().Element(CellStyle).Text(scheme.Product.Name);
                            table.Cell().Element(CellStyle).Text(scheme.RouteOfAdministration.ToString());
                            table.Cell().Element(CellStyle).Text(scheme.Dose);
                            table.Cell().Element(CellStyle).Text(scheme.Timing);
                        }
                    });
            });
        }

        private IContainer DefaultCellStyle(IContainer container, string backgroundColor)
            => container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten1)
                .Background(backgroundColor)
                .PaddingVertical(2)
                .PaddingHorizontal(5)
                .AlignMiddle();

        private IContainer CellStyle(IContainer container)
            => DefaultCellStyle(container, Colors.White).ShowOnce();

        private IContainer HeaderCellStyle(IContainer container)
            => DefaultCellStyle(container, Colors.Grey.Lighten3);
    }
}
