using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Watchlist
{
    [Key] public string Login { get; set; }
    [Key] public string Ticker { get; set; }
    [ForeignKey("Login")] public virtual User User { get; set; }
    [ForeignKey("Ticker")] public virtual Company Company { get; set; }
}