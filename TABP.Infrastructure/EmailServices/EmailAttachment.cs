namespace Infrastructure.EmailServices;

public class EmailAttachment
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
    public string ContentDataType { get; set; }

    public EmailAttachment(string fileName, byte[] data, string contentDataType)
    {
        FileName = fileName;
        Data = data;
        ContentDataType = contentDataType;
    }
}
