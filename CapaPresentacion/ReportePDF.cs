using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion
{
    public class ReportePDF : IDocument
    {
        private readonly List<ReporteCategoria> _items;
        private readonly string _categoria;
        public ReportePDF(List<ReporteCategoria> items, string categoria)
        {
            _items = items;
            _categoria = categoria;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header().Column(column =>
                {
                    column.Item().Text("Reporte de productos")
                        .FontSize(20)
                        .Bold();

                    column.Item().Text($"Categoría seleccionada: {_categoria}")
                        .FontSize(12);

                    column.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}");
                });

                page.Content().PaddingVertical(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); // Nombre
                        columns.RelativeColumn(2); // Categoria
                    });

                    table.Header(header =>
                    {
                        header.Cell().BorderBottom(1).Padding(5).Text("Nombre").Bold();
                        header.Cell().BorderBottom(1).Padding(5).Text("Categoría").Bold();
                    });

                    foreach (var item in _items)
                    {
                        table.Cell().BorderBottom(0.5f).Padding(5).Text(item.Nombre);
                        table.Cell().BorderBottom(0.5f).Padding(5).Text(item.Categoria);
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
            });
        }
    }
}
