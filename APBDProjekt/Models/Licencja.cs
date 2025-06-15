using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Models;

[PrimaryKey(nameof(ClientId), nameof(OprogramowanieId))]
[Table("Licencja")]
public class Licencja
{
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }
    [ForeignKey(nameof(Oprogramowanie))]
    public int OprogramowanieId  { get; set; }
    public Oprogramowanie Oprogramowanie { get; set; }
    [Required]
    public DateTime DataWaznosci { get; set; }
    
}