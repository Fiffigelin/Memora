namespace Memora.Models.DTOs.Vocabulary;

public class UpdateVocabularyListDto
{
  public required Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public required List<UpdateVocabularyDto> Vocabularies { get; set; }
}