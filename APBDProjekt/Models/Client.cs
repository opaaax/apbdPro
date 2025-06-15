using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDProjekt.Models;

[Table("Client")]
public abstract class Client
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Adres { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Email { get; set; }
    [Required]
    [MinLength(9, ErrorMessage = "Numer telefonu musi mieć 9 cyfr")] 
    public required string PhoneNumber { get; set; }
    public ICollection<Licencja> Licencje { get; set; }
    public ICollection<Platnosc> Platnosci { get; set; }
}