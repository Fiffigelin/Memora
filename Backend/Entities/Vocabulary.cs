public class Vocabulary
{
    public Guid Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string Language { get; set; } = "English";

    // fk till listan
    public Guid VocabularyListId { get; set; }
    public VocabularyList? VocabularyList { get; set; }
}
