using Backend.Entities;

public class VocabularyList
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // fk till ef
    public Guid UserId { get; set; }
    public User? User { get; set; }

    // icolletction av ord
    public ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();
}
