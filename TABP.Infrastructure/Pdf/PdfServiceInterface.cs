namespace Infrastructure.Pdf;

public interface PdfServiceInterface
{
    public Task<byte[]> CreatePdfFromHtmlAsync(string htmlContent);
}