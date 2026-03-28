using Driver.Domain.Enums;
using Driver.Domain.Primitives;

namespace Driver.Domain.Entities;

public class Document : Entity
{
    private Document()
    {
    }

    private Document(DocumentType documentType, DateTime expiryDate) : base(Guid.NewGuid())
    {
        DocumentType = documentType;
        ExpiryDate = expiryDate;
    }

    public DocumentType DocumentType { get; private set; }
    private DateTime ExpiryDate { get; }

    public static Document Create(DocumentType documentType, DateTime expiryDate)
    {
        return new Document(documentType, expiryDate);
    }

    public bool IsExpired()
    {
        return ExpiryDate < DateTime.Now;
    }
}