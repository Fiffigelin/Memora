using Backend.Models.DTOs.Vocabulary;

namespace Backend.Models.DTOs.User;

public class UserDto
{
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public List<VocabularyListSummaryDto> VocabularyLists { get; set; } = [];
}