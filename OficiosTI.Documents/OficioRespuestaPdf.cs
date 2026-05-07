using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OficiosTI.Documents;

public class OficioRespuestaPdf : IDocument
{
    private readonly OficioModel _model;

    public OficioRespuestaPdf(OficioModel model)
    {
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.Letter);

            page.MarginHorizontal(60);
            page.MarginTop(10);
            page.MarginBottom(10);

            page.Header().Element(Header);

            page.Content().Element(Content);

            page.Footer().Element(Footer);
        });
    }

    void Header(IContainer container)
    {
        var header = Path.Combine(AppContext.BaseDirectory, "Assets/logo_ssp.png");

        container.Row(row =>
        {
            // imagen institucional
            row.ConstantItem(220)
                .Height(60)
                .Image(header, ImageScaling.FitArea);

            // espacio entre logo y texto
            row.ConstantItem(20);

            // texto de la dirección
            row.RelativeItem()
                .AlignMiddle()
                .Text("DIRECCIÓN DE TECNOLOGÍAS DE LA INFORMACIÓN")
                .FontSize(14)
                .Bold();
        });
    }

    void Content(IContainer container)
    {
        container.Column(col =>
        {
            // Encabezado derecho
            col.Item().AlignRight()
                .Text($"Oficio No. {_model.NumeroOficio}");

            col.Item().AlignRight()
                .Text($"Asunto: {_model.Asunto}");

            col.Item().AlignRight()
                .Text($"En respuesta al oficio: {_model.OficioReferencia}");

            col.Item().AlignRight()
                .Text($"Xalapa-Enríquez, Ver., a {_model.Fecha:dd 'de' MMMM 'de' yyyy}");

            // Línea institucional
            col.Item().PaddingVertical(10)
                .LineHorizontal(1)
                .LineColor(Colors.Grey.Medium);

            // Destinatario
            col.Item().PaddingTop(10)
                .Text(_model.Destinatario)
                .Bold();

            col.Item()
                .Text(_model.CargoDestinatario);

            col.Item()
                .Text("P R E S E N T E");

            // Fundamento legal
            col.Item().PaddingTop(20)
                .Text(_model.FundamentoLegal)
                .Justify();

            // Cuerpo del oficio
            col.Item().PaddingTop(15)
                .Text(_model.Cuerpo)
                .Justify();

            // Firma
            col.Item().PaddingTop(35)
                .AlignCenter()
                .Text("ATENTAMENTE");

            col.Item().PaddingTop(40)
                .AlignCenter()
                .Text(_model.DirectorNombre)
                .Bold();

            col.Item()
                .AlignCenter()
                .Text(_model.DirectorCargo);

            // Copias
            col.Item().PaddingTop(20)
                .Text(_model.Copias)
                .FontSize(9);
        });
    }

    //void Content(IContainer container)
    //{
    //    container.Column(col =>
    //    {
    //        col.Item().PaddingTop(10)
    //            .AlignRight()
    //            .Text($"Xalapa-Enríquez, Ver., a {_model.Fecha:dd 'de' MMMM 'de' yyyy}");

    //        col.Item().PaddingTop(20)
    //            .Text(_model.Destinatario)
    //            .Bold();

    //        col.Item()
    //            .Text(_model.CargoDestinatario);

    //        col.Item()
    //            .Text("P R E S E N T E");

    //        col.Item().PaddingTop(25)
    //            .Text(_model.Cuerpo);

    //        col.Item().PaddingTop(30)
    //            .AlignCenter()
    //            .Text("Atentamente")
    //            .FontSize(12);

    //        col.Item().PaddingTop(40)
    //            .AlignCenter()
    //            .Text(_model.FirmaNombre)
    //            .Bold();

    //        col.Item()
    //            .AlignCenter()
    //            .Text(_model.FirmaCargo);

    //        col.Item().PaddingTop(20)
    //            .Text(_model.Copias)
    //            .FontSize(9);
    //    });
    //}

    void Footer(IContainer container)
    {
        var footer = Path.Combine(AppContext.BaseDirectory, "Assets/logo_veracruz.png");

        container
            .Height(90)
            .AlignCenter()
            .Image(footer, ImageScaling.FitArea);
    }
}