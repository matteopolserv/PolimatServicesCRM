using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface IGeneratePdf
    {
        Task<PdfFileModel> GenerateInvoicePdf(string invoiceId);
        Task<PdfFileModel> GenerateDelegationPdf(string deleagationId);
    }
}
