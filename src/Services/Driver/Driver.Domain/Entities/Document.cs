using Driver.Domain.Enums;
using Driver.Domain.Primitives;

namespace Driver.Domain.Entities;

public class Document : Entity
{
    private Document()
    {
    }

    private Document(Guid id, DocumentType documentType, DateTime expiryDate) : base(id)
    {
        DocumentType = documentType;
        ExpiryDate = expiryDate;
    }

    public DocumentType DocumentType { get; private set; }
    public DateTime ExpiryDate { get; private set; }

    public static Document Create(Guid id, DocumentType documentType, DateTime expiryDate)
    {
        return new Document(id, documentType, expiryDate);
    }


    public bool IsExpired()
    {
        return ExpiryDate < DateTime.UtcNow;
    }

    public void ExtendDocument()
    {
        ExpiryDate = ExpiryDate.AddYears(1);
    }
}