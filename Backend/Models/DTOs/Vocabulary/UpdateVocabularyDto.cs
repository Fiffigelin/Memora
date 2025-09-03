public class UpdateVocabularyDto
{
  public Guid? Id { get; set; }
  public string Word { get; set; } = string.Empty;
  public string Translation { get; set; } = string.Empty;
}