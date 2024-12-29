using Domain.Enums;

namespace TABP.Domain.Entities;

public class Image
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string Url { get; set; }
    public ImgType Type { get; set; }
    public ImgFormat Format { get; set; }
}