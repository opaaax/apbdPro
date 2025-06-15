using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using APBDProjekt.Models.Walidacja;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Models;

[Table("Umowa")]
public class Umowa
{
    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }
    [Required]
    [ForeignKey(nameof(Oprogramowanie))]   
    public int OprogramowanieId { get; set; }
    public Oprogramowanie Oprogramowanie { get; set; }
    [Required]
    public DateTime DataRozpoczecia { get; set; }
    [DaysDifferenceValidation(nameof(DataRozpoczecia))]
    public DateTime DataZakonczenia { get; set; }
    public bool CzyAktywna { get; set; } = true;
    public bool CzyPodpisana { get; set; } = false;
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal  FinalnaCena { get; set; }
    [MaxLength(300)]
    public string? InfoOAktualizacjach { get; set; }
    [AllowedValues(0,1,2,3)]
    public int DodatkoweLataAktualizacji { get; set; }
    [Required]
    [MaxLength(50)]   
    public required string WersjaOprogramowania { get; set; }
    public ICollection<Platnosc> Platnosci { get; set; }
}