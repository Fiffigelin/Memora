namespace Backend.DTOs;

public class UserDto
{
  public Guid Id { get; set; }
  public string Username { get; set; }
  public string Email { get; set; }
  public string PasswordHash { get; set; }
  public List<VocabularyListSummaryDto> VocabularyLists { get; set; }
}