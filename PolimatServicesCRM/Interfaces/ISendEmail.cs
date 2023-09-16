using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface ISendEmail
    {
        Task<bool> SendInvoiceToClient(PdfFileModel pdfFile, string clientEmail, string createdBy);
    }
}
