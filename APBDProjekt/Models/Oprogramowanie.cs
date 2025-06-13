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
    [DataType("numeric")]
    [Precision(10, 2)]
    public double CenaZaRok { get; set; }
    [Required]
    [MaxLength(100)]
    public required string InfoOWersji { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Kategoria { get; set; }
    public ICollection<Znizka> Znizki { get; set; }
}