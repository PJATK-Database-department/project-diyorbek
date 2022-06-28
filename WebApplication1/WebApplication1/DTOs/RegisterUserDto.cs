using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class RegisterUserDto
{
    [Required] [MaxLength(100)] public string Login { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string RepeatPassword { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
}