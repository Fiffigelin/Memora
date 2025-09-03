namespace Backend.Models.DTOs.Vocabulary;

public class CreateVocabularyListDto
{
    public required string Title { get; set; }
    public required string Language { get; set; }
    public List<CreateVocabularyDto> Vocabularies { get; set; } = [];
}