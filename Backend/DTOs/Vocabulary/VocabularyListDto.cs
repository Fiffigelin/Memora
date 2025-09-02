namespace Backend.DTOs.Vocabulary;

public class VocabularyListDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public List<VocabularyDto> Vocabularies { get; set; } = [];
  public string Language { get; set; } = "English";
}