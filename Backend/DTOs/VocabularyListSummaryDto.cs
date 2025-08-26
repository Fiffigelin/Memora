namespace Backend.DTOs;

public class VocabularyListSummaryDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public int VocabularyCount { get; set; }
}
