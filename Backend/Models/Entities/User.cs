namespace Backend.Models.Entities;

public class User
{
	private User() { }

	public User(string username, string email)
	{
		Username = username;
		Email = email;
	}

	public Guid Id { get; private set; }
	public string Username { get; private set; } = null!;
	public string PasswordHash { get; private set; } = null!;
	public string Email { get; private set; } = null!;
	public ICollection<VocabularyList> VocabularyLists { get; set; } = [];

	private void SetUserName(string username)
	{
		if (string.IsNullOrWhiteSpace(username))
			throw new ArgumentException("Username cannot be empty or whitespace.", nameof(username));
		Username = username.Trim();
	}

	public void UpdatePassword(string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(passwordHash))
			throw new ArgumentException("PasswordHash cannot be empty.");
		PasswordHash = passwordHash;
	}

	public void UpdateEmail(string email)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Email cannot be empty.");
		Email = email;
	}

	public void UpdateUsername(string username)
	{
		if (string.IsNullOrWhiteSpace(username))
			throw new ArgumentException("Username cannot be empty.");
		Username = username;
	}
}