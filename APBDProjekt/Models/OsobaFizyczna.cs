using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDProjekt.Models;

[Table("OsobaFizyczna")]
public class OsobaFizyczna : Client
{
    [Required]
    [MaxLength(50)]
    public required string Imie { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Nazwisko { get; set; }
    [Required]
    [MaxLength(11)]
    public required string Pesel { get; init; }
    public bool IsDeleted { get; set; }
}