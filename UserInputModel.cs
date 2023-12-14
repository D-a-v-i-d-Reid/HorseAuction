using HorseAuction;
using System;
using System.ComponentModel.DataAnnotations;

public class UserInputModel
{
    public Guid UserID { get; set; } 

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 25 characters")]
    public string UserName
    {
        get => userName;
        set => userName = value.ToLower();
    }
    private string userName = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    [EnumDataType(typeof(StateAbbreviation), ErrorMessage = "Invalid state abbreviation")]
    public string State
    {
        get => state;
        set => state = value.ToUpper();
    }
    private string state = string.Empty;

    [MaxLength(5)]
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(14, ErrorMessage = "Phone Number should be (555) 555-5555")]
    public string CellPhone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? UserEmail { get; set; }


}

