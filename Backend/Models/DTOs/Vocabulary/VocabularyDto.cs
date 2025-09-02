namespace Backend.Models.DTOs.Vocabulary;

public class VocabularyDto
{
    public Guid Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;

}