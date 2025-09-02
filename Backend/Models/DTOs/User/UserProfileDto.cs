using Backend.Models.DTOs.Vocabulary;

namespace Backend.Models.DTOs.User;

public class UserProfileDto
{
  public Guid Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public List<VocabularyListDto> VocabularyLists { get; set; } = [];
}
