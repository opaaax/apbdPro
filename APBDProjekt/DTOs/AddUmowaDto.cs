using System.ComponentModel.DataAnnotations;
using APBDProjekt.Models.Walidacja;

namespace APBDProjekt.DTOs;

public class AddUmowaDto
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int OprogramowanieId { get; set; }

    [Required]
    public DateTime DataRozpoczecia { get; set; }

    [Required]
    [DaysDifferenceValidation(nameof(DataRozpoczecia))]
    public DateTime DataZakonczenia { get; set; }

    [Required]
    public decimal FinalnyKoszt { get; set; }
    public string? InfoOAktualizacjach { get; set; }
    [AllowedValues(1,2,3)]
    public int DodatkoweLataAktualizacji { get; set; }
    [Required]
    [MaxLength(50)]
    public string WersjaOprogramowania { get; set; }

}