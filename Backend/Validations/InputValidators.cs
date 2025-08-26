using System.Text.RegularExpressions;

namespace Backend.Validations
{
  public static class InputValidators
  {
    public static bool IsValidEmail(string email)
    {
      if (string.IsNullOrWhiteSpace(email)) return false;

      var regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,254}$";
      return Regex.IsMatch(email, regex);
    }

    public static bool IsValidUsername(string username)
    {
      if (string.IsNullOrWhiteSpace(username)) return false;

      var regex = @"^[a-zA-Z0-9_-]{3,20}$";
      return Regex.IsMatch(username, regex);
    }

    // Reminder to myself: "Password must be at least 8 characters, include at least one uppercase letter, one lowercase letter, one number, and one special character."
    public static bool IsValidPassword(string password)
    {
      if (string.IsNullOrWhiteSpace(password)) return false;

      var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,128}$";
      return Regex.IsMatch(password, regex);
    }

    public static bool IsValidName(string name)
    {
      if (string.IsNullOrWhiteSpace(name)) return false;
      var regex = @"^[A-Za-zÀ-ÖØ-öø-ÿ'-]{2,50}$";

      return Regex.IsMatch(name, regex);
    }
    
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
      if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
      var regex = @"^\+?[0-9\s\-\(\)]{5,15}$";

      return Regex.IsMatch(phoneNumber, regex);
    }
  }
}
