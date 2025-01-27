using NReco.PdfGenerator;

namespace Infrastructure.Pdf;

public class PdfService : PdfServiceInterface
{
    public async Task<byte[]> CreatePdfFromHtmlAsync(string html)
    {
        var htmlToPdfConverter = new HtmlToPdfConverter();
        return await Task.FromResult(htmlToPdfConverter.GeneratePdf(html));
    }
}