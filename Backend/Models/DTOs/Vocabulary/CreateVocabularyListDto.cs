namespace Backend.Models.DTOs.Vocabulary;

public class CreateVocabularyListDto
{
    public string Title { get; set; } = string.Empty;
    public string Language { get; set; } = "English";
    public List<CreateVocabularyDto> Vocabularies { get; set; } = [];
}