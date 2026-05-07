using WordInterop = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using OficiosTI.Documents;
using Microsoft.Office.Core;

namespace OficiosTI.Services
{
    public class OficioWordInteropService
    {
        public string Generar(OficioModel model)
        {
            string ruta = Path.Combine(
                Path.GetTempPath(),
                $"oficio_{Guid.NewGuid()}.docx");

            WordInterop.Application wordApp = null;
            WordInterop.Document doc = null;

            try
            {
                wordApp = new WordInterop.Application
                {
                    Visible = false,
                    ScreenUpdating = false,
                    DisplayAlerts = WordInterop.WdAlertLevel.wdAlertsNone
                };

                doc = wordApp.Documents.Add();
                doc.PageSetup.PaperSize = WordInterop.WdPaperSize.wdPaperLetter;

                // =========================
                // HEADER (BANNER OFICIAL)
                // =========================

                string headerPath = Path.Combine(AppContext.BaseDirectory, "Assets", "logo_ssp.png");
                string absoluteHeaderPath = Path.GetFullPath(headerPath);

                if (File.Exists(absoluteHeaderPath))
                {
                    foreach (WordInterop.Section section in doc.Sections)
                    {
                        var header = section.Headers[WordInterop.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                        header.LinkToPrevious = false;

                        var range = header.Range;
                        range.Text = "";
                        range.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

                        var inline = range.InlineShapes.AddPicture(
                            absoluteHeaderPath,
                            LinkToFile: false,
                            SaveWithDocument: true
                        );

                        // 🔥 SOLO esto (NADA de Width)
                        inline.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    }

                    doc.PageSetup.TopMargin = wordApp.CentimetersToPoints(3);
                }

                var sel = wordApp.Selection;

                // =========================
                // ENCABEZADO DERECHO
                // =========================

                sel.Font.Name = "Arial";
                sel.Font.Size = 11;
                sel.Font.Bold = 0;
                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphRight;

                sel.TypeText($"Oficio No. {model.NumeroOficio}");
                sel.TypeParagraph();

                sel.TypeText($"Asunto: {model.Asunto}");
                sel.TypeParagraph();

                sel.TypeText($"En respuesta al oficio: {model.OficioReferencia}");
                sel.TypeParagraph();

                sel.TypeText($"Xalapa-Enríquez, Ver., a {model.Fecha:dd 'de' MMMM 'de' yyyy}");
                sel.TypeParagraph();

                // =========================
                // LÍNEA SEPARADORA
                // =========================

                sel.TypeParagraph();
                sel.Borders[WordInterop.WdBorderType.wdBorderBottom].LineStyle =
                    WordInterop.WdLineStyle.wdLineStyleSingle;
                sel.TypeParagraph();
                sel.Borders[WordInterop.WdBorderType.wdBorderBottom].LineStyle =
                    WordInterop.WdLineStyle.wdLineStyleNone;

                // =========================
                // DESTINATARIO
                // =========================

                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphLeft;

                sel.Font.Bold = 1;
                sel.TypeText(model.Destinatario);
                sel.TypeParagraph();

                sel.Font.Bold = 0;
                sel.TypeText(model.CargoDestinatario);
                sel.TypeParagraph();

                sel.TypeText("P R E S E N T E");
                sel.TypeParagraph();

                sel.TypeParagraph();

                // =========================
                // FUNDAMENTO LEGAL
                // =========================

                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphJustify;

                sel.TypeText(model.FundamentoLegal);
                sel.TypeParagraph();
                sel.TypeParagraph();

                // =========================
                // CUERPO
                // =========================

                sel.TypeText(model.Cuerpo);
                sel.TypeParagraph();
                sel.TypeParagraph();

                // =========================
                // FIRMA
                // =========================

                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

                sel.TypeText("ATENTAMENTE");
                sel.TypeParagraph();
                sel.TypeParagraph();

                sel.Font.Bold = 1;
                sel.TypeText(model.DirectorNombre);
                sel.TypeParagraph();

                sel.Font.Bold = 0;
                sel.TypeText(model.DirectorCargo);
                sel.TypeParagraph();

                sel.TypeParagraph();

                // =========================
                // COPIAS
                // =========================

                sel.Font.Size = 9;
                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphLeft;
                sel.TypeText(model.Copias);

                // =========================
                // FOOTER (LOGO)
                // =========================

                string footerPath = Path.Combine(AppContext.BaseDirectory, "Assets", "logo_veracruz.png");
                string absoluteFooterPath = Path.GetFullPath(footerPath);

                if (File.Exists(absoluteFooterPath))
                {
                    foreach (WordInterop.Section section in doc.Sections)
                    {
                        var footer = section.Footers[WordInterop.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                        footer.LinkToPrevious = false;

                        var range = footer.Range;
                        range.Text = "";
                        range.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

                        var inline = range.InlineShapes.AddPicture(
                            absoluteFooterPath,
                            LinkToFile: false,
                            SaveWithDocument: true,
                            Range: range
                        );

                        inline.LockAspectRatio = MsoTriState.msoTrue;
                        inline.Width = wordApp.CentimetersToPoints(10); // tamaño estable
                    }
                }

                // =========================
                // GUARDAR
                // =========================

                doc.SaveAs2(ruta);

                return ruta;
            }
            catch (COMException ex)
            {
                throw new InvalidOperationException("Error al generar el documento Word", ex);
            }
            finally
            {
                if (doc != null)
                {
                    try { doc.Close(false); } catch { }
                    Marshal.ReleaseComObject(doc);
                }

                if (wordApp != null)
                {
                    try { wordApp.Quit(); } catch { }
                    Marshal.ReleaseComObject(wordApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }
}


//using WordInterop = Microsoft.Office.Interop.Word;
//using System.Runtime.InteropServices;
//using OficiosTI.Documents;

//namespace OficiosTI.Services
//{
//    public class OficioWordInteropService
//    {
//        public string Generar(OficioModel model)
//        {
//            string ruta = Path.Combine(
//                Path.GetTempPath(),
//                $"oficio_{Guid.NewGuid()}.docx");

//            WordInterop.Application wordApp = null;
//            WordInterop.Document doc = null;

//            try
//            {
//                wordApp = new WordInterop.Application
//                {
//                    Visible = false
//                };

//                doc = wordApp.Documents.Add();
//                doc.PageSetup.PaperSize = WordInterop.WdPaperSize.wdPaperLetter;

//                var sel = wordApp.Selection;

//                // =========================
//                // HEADER (LOGO + TEXTO)
//                // =========================

//                string headerPath = Path.Combine(AppContext.BaseDirectory, "Assets/logo_ssp.png");

//                if (File.Exists(headerPath))
//                {
//                    foreach (WordInterop.Section section in doc.Sections)
//                    {
//                        var header = section.Headers[WordInterop.WdHeaderFooterIndex.wdHeaderFooterPrimary];
//                        header.LinkToPrevious = false;

//                        var range = header.Range;
//                        range.Text = "";
//                        range.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

//                        // 🔥 Insertar como InlineShape (NO convertir)
//                        var inline = range.InlineShapes.AddPicture(headerPath);

//                        // 🔥 Ajuste seguro (esto NO truena)
//                        inline.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
//                        inline.Width = wordApp.CentimetersToPoints(19);
//                    }

//                    // 🔥 Ajustar margen superior
//                    doc.PageSetup.TopMargin = wordApp.CentimetersToPoints(3);
//                }

//                //if (File.Exists(headerPath))
//                //{
//                //    var table = doc.Tables.Add(sel.Range, 1, 2);
//                //    table.Borders.Enable = 0;

//                //    // Logo
//                //    var cellLogo = table.Cell(1, 1).Range;
//                //    var img = cellLogo.InlineShapes.AddPicture(headerPath);
//                //    img.Width = 210;
//                //    img.Height = 50;
//                //    cellLogo.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphLeft;

//                //    // Texto
//                //    var cellText = table.Cell(1, 2).Range;
//                //    cellText.Text = "DIRECCIÓN DE TECNOLOGÍAS DE LA INFORMACIÓN";
//                //    cellText.Font.Size = 14;
//                //    cellText.Font.Bold = 1;
//                //    cellText.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphRight;

//                //    sel.MoveDown();
//                //    sel.TypeParagraph();
//                //}

//                // =========================
//                // ENCABEZADO DERECHO
//                // =========================

//                sel.Font.Size = 11;
//                sel.Font.Bold = 0;
//                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphRight;

//                sel.TypeText($"Oficio No. {model.NumeroOficio}");
//                sel.TypeParagraph();

//                sel.TypeText($"Asunto: {model.Asunto}");
//                sel.TypeParagraph();

//                sel.TypeText($"En respuesta al oficio: {model.OficioReferencia}");
//                sel.TypeParagraph();

//                sel.TypeText($"Xalapa-Enríquez, Ver., a {model.Fecha:dd 'de' MMMM 'de' yyyy}");
//                sel.TypeParagraph();

//                // =========================
//                // LÍNEA
//                // =========================

//                sel.TypeParagraph();
//                sel.Borders[WordInterop.WdBorderType.wdBorderBottom].LineStyle =
//                    WordInterop.WdLineStyle.wdLineStyleSingle;
//                sel.TypeParagraph();
//                sel.Borders[WordInterop.WdBorderType.wdBorderBottom].LineStyle =
//                    WordInterop.WdLineStyle.wdLineStyleNone;

//                // =========================
//                // DESTINATARIO
//                // =========================

//                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphLeft;

//                sel.Font.Bold = 1;
//                sel.TypeText(model.Destinatario);
//                sel.TypeParagraph();

//                sel.Font.Bold = 0;
//                sel.TypeText(model.CargoDestinatario);
//                sel.TypeParagraph();

//                sel.TypeText("P R E S E N T E");
//                sel.TypeParagraph();

//                sel.TypeParagraph();

//                // =========================
//                // FUNDAMENTO LEGAL
//                // =========================

//                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphJustify;

//                sel.TypeText(model.FundamentoLegal);
//                sel.TypeParagraph();

//                sel.TypeParagraph();

//                // =========================
//                // CUERPO
//                // =========================

//                sel.TypeText(model.Cuerpo);
//                sel.TypeParagraph();

//                sel.TypeParagraph();

//                // =========================
//                // FIRMA
//                // =========================

//                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

//                sel.TypeText("ATENTAMENTE");
//                sel.TypeParagraph();
//                sel.TypeParagraph();

//                sel.Font.Bold = 1;
//                sel.TypeText(model.DirectorNombre);
//                sel.TypeParagraph();

//                sel.Font.Bold = 0;
//                sel.TypeText(model.DirectorCargo);
//                sel.TypeParagraph();

//                sel.TypeParagraph();

//                // =========================
//                // COPIAS
//                // =========================

//                sel.Font.Size = 9;
//                sel.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphLeft;

//                sel.TypeText(model.Copias);

//                // =========================
//                // FOOTER (LOGO)
//                // =========================

//                string footerPath = Path.Combine(AppContext.BaseDirectory, "Assets/logo_veracruz.png");

//                if (File.Exists(footerPath))
//                {
//                    foreach (WordInterop.Section section in doc.Sections)
//                    {
//                        var footer = section.Footers[WordInterop.WdHeaderFooterIndex.wdHeaderFooterPrimary];

//                        footer.LinkToPrevious = false;

//                        var range = footer.Range;
//                        range.Text = "";
//                        range.ParagraphFormat.Alignment = WordInterop.WdParagraphAlignment.wdAlignParagraphCenter;

//                        range.InlineShapes.AddPicture(footerPath);

//                        var img = range.InlineShapes[range.InlineShapes.Count];

//                        // 🔥 CLAVE: usar escala en lugar de Width
//                        img.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
//                        img.ScaleWidth = 60;   // porcentaje
//                        img.ScaleHeight = 60;  // porcentaje
//                    }
//                }

//                // =========================
//                // GUARDAR
//                // =========================

//                doc.SaveAs2(ruta);

//                return ruta;
//            }
//            finally
//            {
//                if (doc != null)
//                {
//                    doc.Close(false);
//                    Marshal.ReleaseComObject(doc);
//                }

//                if (wordApp != null)
//                {
//                    wordApp.Quit();
//                    Marshal.ReleaseComObject(wordApp);
//                }

//                GC.Collect();
//                GC.WaitForPendingFinalizers();
//            }
//        }
//    }
//}
