using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class LoginUserDto
{
    [Required] public string Login { get; set; }
    [Required] public string Password { get; set; }
}