using Backend.DTOs.Vocabulary;

namespace Backend.DTOs.User;

public class UserDto
{
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public List<VocabularyListSummaryDto> VocabularyLists { get; set; } = [];
}