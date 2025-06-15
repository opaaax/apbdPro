using System.ComponentModel.DataAnnotations;

namespace APBDProjekt.DTOs;

public class AddClientDto
{
    [MaxLength(50)]
    public string? Imie { get; set; }
    [MaxLength(50)]
    public string? Nazwisko { get; set; }
    [MaxLength(11)]
    public string? Pesel { get; set; }
    [MaxLength(10)]
    public string? Krs { get; set; }
    [MaxLength(50)]
    public string? Nazwa { get; set; }
    [MaxLength(100)]
    [Required]
    public string Adres { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MaxLength(9, ErrorMessage = "Numer telefonu musi mieć 9 cyfr")]   
    public string PhoneNumber { get; set; }
}