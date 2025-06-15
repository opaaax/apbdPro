using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDProjekt.Models;

[Table("Firma")]
public class Firma : Client
{
    [Required]
    [MaxLength(50)]
    public required string Nazwa { get; set; }
    [Required]
    [MaxLength(10)]
    public required string Krs { get; init; }
}