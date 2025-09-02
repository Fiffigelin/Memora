namespace Backend.DTOs.Vocabulary;

public class CreateVocabularyDto
{
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
}