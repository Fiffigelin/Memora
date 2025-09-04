namespace Backend.Models.DTOs.Vocabulary;

public class VocabularyListDto
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public List<VocabularyDto> Vocabularies { get; set; } = [];
  public string Language { get; set; } = "English";
  public DateTime CreatedAt { get; set; }
}