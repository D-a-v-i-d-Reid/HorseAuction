using System;
using System.ComponentModel.DataAnnotations;

public class UserInputModel
{
    public int UserID { get; set; } = 0;

    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(25, ErrorMessage = "UserName cannot be longer than 25 characters")]
    public string UserName { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    [MaxLength(2, ErrorMessage = "State should be a two-digit code")]
    public string State { get; set; }
    [MaxLength(5)]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(14, ErrorMessage = "Phone Number should be (555) 555-5555")]
    public string CellPhone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? UserEmail { get; set; }


}

