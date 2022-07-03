using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Company
{
    [Key] [MaxLength(30)] public string Ticker { get; set; }

    [Required] public string Info { get; set; }
    [Required] public string Prices { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }
}