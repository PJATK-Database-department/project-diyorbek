using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    [Key] [MaxLength(100)] public string Login { get; set; }

    [Required] public string PasswordHash { get; set; }
    [Required] public string PasswordSalt { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
}