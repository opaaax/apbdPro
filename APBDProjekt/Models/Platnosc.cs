using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Models;

[Table("Platnosc")]
public class Platnosc
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Umowa))]
    public int UmowaId { get; set; }
    public Umowa Umowa { get; set; }
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Kwota { get; set; }
    public bool CzyZwrocona { get; set; }
    
}