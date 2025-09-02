namespace Backend.Models.DTOs.Vocabulary;

public class VocabularyListSummaryDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public int VocabularyCount { get; set; }
}
