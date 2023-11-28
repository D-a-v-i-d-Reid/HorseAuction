using System;
using System.Text.RegularExpressions;

public class Users
{
    public int UserID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    private string? _userEmail;

    public string? UserEmail
    {
        get => _userEmail;
        set
        {
            if (IsvalidEmail(value))
            {
                _userEmail = value;
            }
            else
            {
                throw new ArgumentException("Invalid email format");
            }

        }
    }
    private bool IsvalidEmail(string? email)
    {
        if (email == null)
        {
            return false;
        }
        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(emailPattern, email);
    }
}
