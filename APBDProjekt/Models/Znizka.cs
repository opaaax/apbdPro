using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDProjekt.Models;

[Table("Znizka")]
public class Znizka
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]   
    public required string Nazwa { get; set; }
    [Required]
    public int WartoscWProcentach { set; get; }
    [Required]
    public DateTime DataRozpoczecia { get; set; }
    [Required]
    public DateTime DataZakonczenia { get; set; }
    [ForeignKey(nameof(Oprogramowanie))]
    public int IdOprogramowania { get; set; }
    public Oprogramowanie Oprogramowanie { get; set; }
    
}