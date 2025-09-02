namespace Backend.Models.Entities;

public class VocabularyList
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Language { get; set; } = "English";

    // fk till ef
    public Guid UserId { get; set; }
    public required User User { get; set; }

    // icolletction av ord
    public ICollection<Vocabulary> Vocabularies { get; set; } = [];
}
