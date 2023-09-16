using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Models;


namespace PolimatServicesCRM.Services
{
    public class SendEmail : ISendEmail
    {
        public async Task<bool> SendInvoiceToClient(PdfFileModel invoice, string clientEmail, string createdBy)
        {
            string subject = $"Faktura Vat";

            string replyTo = "matteo@polserv.pro";
            string text = "Dzień dobry, w załączeniu przesyłam fakturę VAT za wykonane usługi.<p></p><p>Pozdrawiam</p><p>"+ createdBy + "</p><p></p><p></p><p></p>\n\n\n\n <footer> <style>p{margin-left: 5px;margin-block-start: 0.4em;margin-block-end: 0.4em;}</style><div style=\"display: inline-block;\"><a href=\"https://polserv.pro\" target=\"_blank\"><img src=\"https://polserv.pro/footer/logo.png\" height=\"300\" width=\"300\" alt=\"Logo Polimat Services Spółka z ograniczoną odpowiedzialnością\"></a></div><div style=\"display: inline-block;\"><h4>POLIMAT SERVICES SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ</h4><p>Kielnarowa 217d</p><p>36-020 Tyczyn</p><p>Tel. 662 645 902</p><p>e-mail: <a href=\"mailto:kontakt@polserv.pro\">kontakt@polserv.pro</a></p><p>strona www: <a href=\"https://polserv. pro\">https://polserv.pro</a> </p><p>XII Wydział Gospodarczy Krajowego Rejestru Sądowego, Sąd Rejonowy W Rzeszowie </p><p>Numer KRS: 0001004384</p><p>NIP: 813-388-93-24</p><p>REGON: 523773898</p><p>Kapitał założycielski: 5000 zł</p><p>Numer konta bankowego: 47 2530 0008 2051 1075 1153 0001</p></div></footer>";
            try
            {
                await Task.Run(async () =>
                {
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress($"Polimat Services Spółka z. o. o.", "matteo@polserv.pro"));
                    email.To.Add(MailboxAddress.Parse(clientEmail));
                    email.ReplyTo.Add(MailboxAddress.Parse(replyTo));
                    email.Subject = subject;


                    var builder = new BodyBuilder
                    {
                        HtmlBody = text
                    };
                    Stream stream = new MemoryStream(invoice.Data);
                    ContentType contentType = new(invoice.Extension, invoice.Extension);

                    await builder.Attachments.AddAsync(invoice.Name, stream);


                    email.Body = builder.ToMessageBody();



                    // send email

                    using var smtp = new SmtpClient();
                    smtp.Connect("s6.cyber-folks.pl", 465, SecureSocketOptions.SslOnConnect);
                    smtp.Authenticate("matteo@polserv.pro", "Verry Strong Password");
                    smtp.Send(email);
                    smtp.Disconnect(true);

                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }
        }
    }
}
