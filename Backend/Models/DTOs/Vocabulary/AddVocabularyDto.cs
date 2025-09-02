namespace Backend.Models.DTOs.Vocabulary;

public class AddVocabularyDto
{
  public Guid VocabularyListId { get; set; }
  public string Word { get; set; } = string.Empty;
  public string Translation { get; set; } = string.Empty;
}
