using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Models;

[Table("Oprogramowanie")]
public class Oprogramowanie
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Nazwa { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal CenaZaRok { get; set; }
    [Required]
    [MaxLength(100)]
    public required string InfoOWersji { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Kategoria { get; set; }
    public ICollection<Licencja> Licencje { get; set; }
    public ICollection<Znizka> Znizki { get; set; }
}