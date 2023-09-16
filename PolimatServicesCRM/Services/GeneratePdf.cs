using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Globalization;
using System.Reflection.Metadata;
using Font = iTextSharp.text.Font;
using PolimatServicesCRM.Models;
using Rectangle = iTextSharp.text.Rectangle;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
using PolimatServicesCRM.Data.Migrations;
using static iTextSharp.text.pdf.PdfDocument;
using System.Security.Cryptography.Xml;

namespace PolimatServicesCRM.Services
{
    public class GeneratePdf : IGeneratePdf
    {
        private readonly IInvoicesRepository _invoices;
        private readonly IClientsRepository _clients;
        private readonly IDelegationsRepository _delegations;
        public GeneratePdf(IInvoicesRepository invoices, IClientsRepository clients, IDelegationsRepository delegations)
        {
            _invoices = invoices;
            _clients = clients;
            _delegations = delegations;

        }
        public async Task<PdfFileModel> GenerateDelegationPdf(string delegationId)
        {
            DelegationModel delegation = await _delegations.GetDelegationById(delegationId);
            using MemoryStream ms = new();

            using iTextSharp.text.Document doc = new(PageSize.A4.Rotate(), 20, 20, 20, 20);

            using PdfWriter pdfwriter = PdfWriter.GetInstance(doc, ms);

            DateTime mydate = DateTime.Today;
            int myyear = mydate.Year;
            pdfwriter.CloseStream = false;
            doc.Open();

            string fontPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "fonts", "Lucon.ttf");
            string fontName = "Lucida";

            BaseFont myfont;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding(1252);
            try
            {
                if (!FontFactory.IsRegistered(fontName)) FontFactory.Register(fontPath, fontName);
                myfont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                myfont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);
            }

            Paragraph myspace = new("\n", new Font(myfont, 6))
            {
                Alignment = Element.ALIGN_CENTER
            };
            //Paragraph paradate = new("Kielnarowa, " + date, new Font(myfont, 9))
            //{
            //    Alignment = Element.ALIGN_RIGHT
            // };
            //doc.Add(paradate);
            string logoPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "images", "logo2.jpg");
            var logo = iTextSharp.text.Image.GetInstance(logoPath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleAbsolute(100f, 100f);
            logo.SetAbsolutePosition(doc.PageSize.Width - 200, doc.PageSize.Height - 120f);
            doc.Add(logo);

            Paragraph myname = new("Polimat Services Spółka z o. o.", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(myname);
            Paragraph myaddress = new("Kielnarowa 217d", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(myaddress);
            Paragraph mycity = new("36-020 Tyczyn", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mycity);

            Paragraph myphone = new("Telefon: +48 662 645 902, e-mail: kontakt@polserv.pro", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(myphone);
            Paragraph myemail = new("E-mail: kontakt@polserv.pro", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            //doc.Add(myemail);
            Paragraph mynipregon = new("NIP: 813-388-93-24 REGON: 523773898", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mynipregon);
            Paragraph mykrs = new("XII Wydział Gospodarczy Krajowego Rejestru Sądowego, \r\nSąd Rejonowy W Rzeszowie Numer KRS: 0001004384", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mykrs);
            Paragraph mycapital = new("Kapitał założycielski: 5000 zł", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mycapital);


            Paragraph parmybankaccountnumber = new("Konto Bankowe Nest Bank:\nPL47 2530 0008 2051 1075 1153 0001", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(parmybankaccountnumber);
            doc.Add(myspace);

            Paragraph myDelegation = new("Polecenie wyjazdu służbowego " + delegation.DelegationId[0..5] + "/" + myyear.ToString(), new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(myDelegation);
            doc.Add(myspace);

            PdfPTable mytable = new(8);
            int[] intTblWidth = { 35, 35, 35, 35, 35, 35, 35, 35 };
            mytable.SetWidths(intTblWidth);
            PdfPCell cellWorkerName = new(new Phrase("Imię i nazwisko delegowanego", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellWorkerPost = new PdfPCell(new Phrase("Stanowisko delegowanego", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellDepartureDate = new PdfPCell(new Phrase("Data wyjazdu", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellComebackDate = new PdfPCell(new Phrase("Data powrotu", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellDeparturePlace = new PdfPCell(new Phrase("Miejsce wyjazdu", new Font(myfont, 7)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellDestination = new PdfPCell(new Phrase("Miejsce docelowe", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellPurpose = new PdfPCell(new Phrase("Cel wyjazdu", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellTransport = new PdfPCell(new Phrase("Środek transportu", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable.AddCell(cellWorkerName);
            mytable.AddCell(cellWorkerPost);
            mytable.AddCell(cellDepartureDate);
            mytable.AddCell(cellComebackDate);
            mytable.AddCell(cellDeparturePlace);
            mytable.AddCell(cellDestination);
            mytable.AddCell(cellPurpose);
            mytable.AddCell(cellTransport);

            PdfPCell cellWorkerName_1 = new(new Phrase(delegation.DelegatedName, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellWorkerPost_1 = new PdfPCell(new Phrase(delegation.DelegatedPost, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            PdfPCell cellDepartureDate_1 = new(new Phrase(delegation.DelegationStartDate.ToString("d"), new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellComebackDate_1 = new(new Phrase(delegation.DelegationEndDate.ToString("d"), new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellDeparturePlace_1 = new PdfPCell(new Phrase(delegation.DeleagationDeparturePlace, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellDestination_1 = new(new Phrase(delegation.DeleagationArrivngPlace, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellPurpose_1 = new(new Phrase(delegation.DelegationPurpose, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };

            PdfPCell cellTransport_1 = new(new Phrase(delegation.Vechicle, new Font(myfont, 7)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,

            };
            mytable.AddCell(cellWorkerName_1);
            mytable.AddCell(cellWorkerPost_1);
            mytable.AddCell(cellDepartureDate_1);
            mytable.AddCell(cellComebackDate_1);
            mytable.AddCell(cellDeparturePlace_1);
            mytable.AddCell(cellDestination_1);
            mytable.AddCell(cellPurpose_1);
            mytable.AddCell(cellTransport_1);

            doc.Add(mytable);
            doc.Add(myspace);
            doc.Add(myspace);

            int[] intTblWidth3 = { 90, 90, 110 };
            PdfPTable mytable4 = new(3);

            mytable4.SetWidths(intTblWidth3);

            PdfPCell cellDelegatingName = new(new Phrase("Imię i nazwisko zlecającego wyjazd", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            PdfPCell cellDelegatingPost = new(new Phrase("Stanowisko zlecającego wyjazd", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellSignature = new(new Phrase("Data i podpis", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable4.AddCell(cellDelegatingName);
            mytable4.AddCell(cellDelegatingPost);
            mytable4.AddCell(cellSignature);


            PdfPCell cellDelegatingName_1 = new(new Phrase(delegation.DelegatingName, new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            PdfPCell cellDelegatingPost_1 = new(new Phrase(delegation.DelegatingPost, new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellSignature_1 = new(new Phrase("\n\n\n ", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable4.AddCell(cellDelegatingName_1);
            mytable4.AddCell(cellDelegatingPost_1);
            mytable4.AddCell(cellSignature_1);

            doc.Add(mytable4);
            doc.Add(myspace);
            doc.Add(myspace);
            doc.Add(myspace);
            Paragraph myReckoning = new("Rozliczenie wyjazdu służbowego " + delegation.DelegationId[0..5] + "/" + myyear.ToString(), new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };

            doc.Add(myReckoning);
            doc.Add(myspace);
            PdfPTable mytable3 = new(8);
            mytable3.SetWidths(intTblWidth);

            PdfPCell cellAdvance = new(new Phrase("Wypłacona zaliczka", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellDistance = new(new Phrase("Przejechany dystans", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellKilometrageAmount = new(new Phrase("Kwota za przejechany dystans", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };


            PdfPCell cellSettlementTime = new(new Phrase("Czas w delegacji", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            PdfPCell cellSettlementAmount = new(new Phrase("Należność z racji diety", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellTotalAmount = new(new Phrase("Łącznie do wypłaty", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            mytable3.AddCell(cellAdvance);
            mytable3.AddCell(cellDistance);
            mytable3.AddCell(cellKilometrageAmount);
            mytable3.AddCell(cellDepartureDate);
            mytable3.AddCell(cellComebackDate);
            mytable3.AddCell(cellSettlementTime);
            mytable3.AddCell(cellSettlementAmount);
            mytable3.AddCell(cellTotalAmount);

            PdfPCell cellAdvance_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellDistance_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellKilometrageAmount_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            PdfPCell cellDepartureDate_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellComebackDate_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellSettlementTime_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            PdfPCell cellSettlementAmount_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            PdfPCell cellTotalAmount_2 = new(new Phrase("\n\n\n", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };

            mytable3.AddCell(cellAdvance_2);
            mytable3.AddCell(cellDistance_2);
            mytable3.AddCell(cellKilometrageAmount_2);
            mytable3.AddCell(cellDepartureDate_2);
            mytable3.AddCell(cellComebackDate_2);
            mytable3.AddCell(cellSettlementTime_2);
            mytable3.AddCell(cellSettlementAmount_2);
            mytable3.AddCell(cellTotalAmount_2);

            doc.Add(mytable3);
            doc.Add(myspace);



            Paragraph workerSignature = new("Data i podpis delegowanego: ", new Font(myfont, 10))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(workerSignature);

            doc.Add(myspace);
            doc.Add(myspace);
            doc.Add(myspace);
            doc.Add(myspace);
            doc.Add(myspace);


            int[] intTblWidth2 = { 80, 80 };
            PdfPTable mytable2 = new(2);

            mytable2.SetWidths(intTblWidth2);
            PdfPCell cellSettlementCost = new(new Phrase("Dobowa stawka żywieniowa", new Font(myfont, 10)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellCostPerKm = new(new Phrase("Stawka za przejechanie 1 kilometra", new Font(myfont, 10)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable2.AddCell(cellSettlementCost);
            mytable2.AddCell(cellCostPerKm);

            PdfPCell cellSettlementCost_1 = new(new Phrase(delegation.DeleagationPerDiemRate + " PLN", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellCostPerKm_1 = new(new Phrase(delegation.DistanceDistanceRate.ToString("0.0000", CultureInfo.GetCultureInfo("pl-PL")) + " PLN", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable2.AddCell(cellSettlementCost_1);
            mytable2.AddCell(cellCostPerKm_1);

            doc.Add(mytable2);
            doc.Close();




            var constant = ms.ToArray();
            
            string file_name_pdf = "Delegacja-" + delegation.DelegationId[0..5] + "-" + myyear.ToString() + ".pdf";
            PdfFileModel pdfFile = new()
            {
                Name = file_name_pdf,
                Data = constant,
                Extension = "pdf"
        };
            pdfwriter.Close();
            return pdfFile;
        }

        public async Task<PdfFileModel> GenerateInvoicePdf(string invoiceId)
        {
            InvoiceModel invoice = await _invoices.GetInvoiceById(invoiceId);
            ClientModel client = await _invoices.GetClientByInvoiceId(invoiceId);
            using MemoryStream ms = new();

            using iTextSharp.text.Document doc = new(PageSize.A4, 20, 20, 20, 20);

            using PdfWriter pdfwriter = PdfWriter.GetInstance(doc, ms);

            DateTime mydate = DateTime.Today;
            int myyear = mydate.Year;
            pdfwriter.CloseStream = false;
            doc.Open();
            string fontPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "fonts", "Lucon.ttf");
            string fontName = "Lucida";
  
            BaseFont myfont;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding(1252);
            try
            {
                if(!FontFactory.IsRegistered(fontName)) FontFactory.Register(fontPath, fontName);
                myfont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);            

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                myfont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, true);
            }
            
            Paragraph myspace = new("\n", new Font(myfont, 6))
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph paradate = new($"Kielnarowa, {invoice.CretedTime.ToString("D", new CultureInfo("pl-PL"))}", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_RIGHT
            };

            doc.Add(paradate);
            string logoPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "images", "logo2.jpg");
            var logo = iTextSharp.text.Image.GetInstance(logoPath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleAbsolute(100f, 100f);
            logo.SetAbsolutePosition(doc.PageSize.Width - 350, doc.PageSize.Height - 120f);
            doc.Add(logo);

            Paragraph parahead = new("Sprzedawca:", new Font(myfont, 8))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(parahead);
            Paragraph myname = new("Polimat Services Spółka z o. o.", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(myname);
            Paragraph myaddress = new("Kielnarowa 217d", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(myaddress);
            Paragraph mycity = new("36-020 Tyczyn", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mycity);

            Paragraph myphone = new("Telefon: +48 662 645 902, email: kontakt@polserv.pro", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            //doc.Add(myphone);
            Paragraph myemail = new("E-mail: kontakt@polserv.pro", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            Paragraph mynipregon = new("NIP: 813-388-93-24 REGON: 523773898", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(mynipregon);
            Paragraph mykrs = new("XII Wydział Gospodarczy Krajowego Rejestru Sądowego, \r\nSąd Rejonowy W Rzeszowie Numer KRS: 0001004384", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            //doc.Add(mykrs);
            Paragraph mycapital = new("Kapitał założycielski: 5000 zł", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            //doc.Add(mycapital);


            Paragraph parmybankaccountnumber = new("Konto Bankowe Nest Bank:\nPL47 2530 0008 2051 1075 1153 0001", new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_LEFT
            };
            doc.Add(parmybankaccountnumber);
            doc.Add(myspace);
            Paragraph paraname = new("Nabywca: \n" + client.ClientName, new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(paraname);
            Paragraph paraaddress = new(client.ClientAddress, new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(paraaddress);
            Paragraph parcity = new(client.ClientZip + " " + client.ClientCity, new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(parcity);

            Paragraph paraphone = new("Tel.: " + client.ClientPhoneNumber + ", email: " + client.ClientEmail, new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            //doc.Add(paraphone);

            Paragraph paranip = new("NIP: " + client.ClientNip, new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(paranip);



            doc.Add(myspace);
            Paragraph myinvoice = new("Faktura VAT: PolServ/" + invoice.InvoiceNumber + "/" + myyear.ToString(), new Font(myfont, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(myinvoice);
            doc.Add(myspace);

            PdfPTable mytable = new(8);
            int[] intTblWidth = { 8, 50, 25, 15, 15, 25, 30, 30 };
            mytable.SetWidths(intTblWidth);
            PdfPCell cellnumber = new(new Phrase("Lp", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell celldesc = new PdfPCell(new Phrase("Opis usługi", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellamount = new PdfPCell(new Phrase("Cena netto jednostki", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellquantity = new PdfPCell(new Phrase("Ilość", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellvat = new PdfPCell(new Phrase("Stawka VAT", new Font(myfont, 7)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellvatam = new PdfPCell(new Phrase("Kwota VAT", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellamountnetto = new PdfPCell(new Phrase("Kwota netto", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellamountvat = new PdfPCell(new Phrase("Kwota brutto", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable.AddCell(cellnumber);
            mytable.AddCell(celldesc);
            mytable.AddCell(cellamount);
            mytable.AddCell(cellquantity);
            mytable.AddCell(cellvat);
            mytable.AddCell(cellvatam);
            mytable.AddCell(cellamountnetto);
            mytable.AddCell(cellamountvat);
            for (int i = 0; i < invoice.Products.Count; i++)
            {
                PdfPCell cellnumber_1 = new(new Phrase((i + 1).ToString(), new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                string descriptionstr = invoice.Products[i].ProductServiceName;
                PdfPCell celldesc_1 = new PdfPCell(new Phrase(descriptionstr, new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                decimal amountdec = invoice.Products[i].ProductServiceNetAmmount;
                string amounstr = amountdec.ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
                //amounstr = amounstr.Replace(".", ",");
                PdfPCell cellamount_1 = new(new Phrase(amounstr, new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                int quantityint = invoice.Products[i].ProductServiceNumber;
                PdfPCell cellquantity_1 = new(new Phrase(invoice.Products[i].ProductServiceNumber.ToString(), new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };

                PdfPCell cellvat_1 = new PdfPCell(new Phrase(invoice.Products[i].ProductServiceVat.ToString() + "%", new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                int vatint = invoice.Products[i].ProductServiceVat;
                decimal vatam = invoice.Products[i].ProductServiceVatAmmount;
                string vatamstr = vatam.ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
                //vatamstr = vatamstr.Replace(".", ",");
                PdfPCell cellvatam_1 = new(new Phrase(vatamstr, new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                decimal amountnetto = invoice.Products[i].ProductServiceNetAmmount;
                string amountnettostr = amountnetto.ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
                //amoountvatstr = amoountvatstr.Replace(".", ",");
                PdfPCell cellamountnetto_1 = new(new Phrase(amountnettostr, new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                decimal amountwithvat = invoice.Products[i].ProductServiceTotal;
                string amountvatstr = amountwithvat.ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
                //amoountvatstr = amoountvatstr.Replace(".", ",");
                PdfPCell cellamountvat_1 = new(new Phrase(amountvatstr, new Font(myfont, 7)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,

                };
                mytable.AddCell(cellnumber_1);
                mytable.AddCell(celldesc_1);
                mytable.AddCell(cellamount_1);
                mytable.AddCell(cellquantity_1);
                mytable.AddCell(cellvat_1);
                mytable.AddCell(cellvatam_1);
                mytable.AddCell(cellamountnetto_1);
                mytable.AddCell(cellamountvat_1);
            }
            doc.Add(mytable);
            doc.Add(myspace);
            int[] intTblWidth3 = { 102, 103 };
            PdfPTable mytable4 = new(2);

            mytable4.SetWidths(intTblWidth3);
            PdfPCell celltotalvat = new(new Phrase("Vat łącznie: ", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };
            decimal vattotal = invoice.TotalVatAmmount;


            string vatamounttotal = vattotal.ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
            //amountvatstr = amountvatstr.Replace(".", ",");
            PdfPCell celltotalvatamount = new(new Phrase(vatamounttotal, new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 1f,
                BorderWidthLeft = 0,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable4.AddCell(celltotalvat);
            mytable4.AddCell(celltotalvatamount);
            doc.Add(mytable4);
            PdfPTable mytable3 = new(2);
            mytable3.SetWidths(intTblWidth3);
            PdfPCell celltotal = new(new Phrase("Łącznie brutto do zapłaty: ", new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 1f,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER


            };


            decimal amounttotal = invoice.TotalNetAmmount;

            string amounttotaltithvatstr = (amounttotal + vattotal).ToString("0.00", CultureInfo.GetCultureInfo("pl-PL")) + " PLN";
            //amountvatstr = amountvatstr.Replace(".", ",");
            PdfPCell celltotalamount = new(new Phrase(amounttotaltithvatstr, new Font(myfont, 9)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 1f,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 1f,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable3.AddCell(celltotal);
            mytable3.AddCell(celltotalamount);
            doc.Add(mytable3);
            doc.Add(myspace);

            int[] intTblWidth2 = { 105, 105 };
            PdfPTable mytable2 = new(2);

            mytable2.SetWidths(intTblWidth2);
            PdfPCell cellrealisationdate = new(new Phrase($"Data realizacji zlecenia:\n {invoice.CretedTime.ToString("D", new CultureInfo("pl-PL"))}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellsetdate = new(new Phrase($"Data wystawienia faktury:\n {invoice.CretedTime.ToString("D", new CultureInfo("pl-PL"))}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable2.AddCell(cellrealisationdate);
            mytable2.AddCell(cellsetdate);
            PdfPCell cellpaymentdate = new(new Phrase($"Termin płatności: \n {invoice.PaymentDeadline.ToString("D", new CultureInfo("pl-PL"))}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellpaymentmethod = new(new Phrase($"Forma płatności: \n {invoice.PaymentMethod}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable2.AddCell(cellpaymentdate);
            mytable2.AddCell(cellpaymentmethod);

            PdfPCell cellreceived = new(new Phrase($"Fakturę otrzymał:\n {invoice.RceivedBy}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            PdfPCell cellinvoiceset = new(new Phrase($"Fakturę wystawił: \n {invoice.CreatedBy}", new Font(myfont, 8)))
            {
                Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidthBottom = 0,
                BorderWidthTop = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER

            };
            mytable2.AddCell(cellreceived);
            mytable2.AddCell(cellinvoiceset);

            doc.Add(mytable2);
            doc.Add(myspace);
            Paragraph pardeclaration = new("Dokument wystawiony automatycznie nie wymagający podpisu. Podstawa prawna: Ustawa z dnia 11 marca 2014 o podatku od towarów i usług art.106e", new Font(myfont, 8))
            {
                Alignment = Element.ALIGN_CENTER


            };
            doc.Add(pardeclaration);



            doc.Close();
            pdfwriter.Close();
            var constant = ms.ToArray();

            string file_name_pdf = "FVAT-PolServ-" + invoice.InvoiceNumber + "-" + myyear.ToString() + ".pdf";
            PdfFileModel file = new()
            {
                Name = file_name_pdf,
                Extension = "pdf",
                Data = constant,
            };
            return file;
        }

        //public async Task<InvoiceFileModel> GenerateInvoicePdfSharp(string invoiceId)
        //{
        //    InvoiceModel invoice = await _invoices.GetInvoiceById(invoiceId);
        //    ClientModel client = await _invoices.GetClientByInvoiceId(invoiceId);
        //    PdfDocument document = new();
        //    XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
        //    //XFont font = new XFont("Arial", 10, XFontStyle.Regular, options);
        //    document.Info.Title = $"Polimat Services Spółka z o. o. Fkatura VAT {invoice.InvoiceNumber}/{DateTime.Now.Year}";

        //    PdfPage page = document.AddPage();
        //    MemoryStream ms = new();
        //    InvoiceFileModel invoiceFile = new();
        //    document.Save(ms, true);
        //    Process.Start(new ProcessStartInfo());
        //    invoiceFile.Data = ms.ToArray();
        //    invoiceFile.Extension = "pdf";
        //    invoiceFile.Name = "test.pdf";
        //    return invoiceFile;
        //}
    }
}
