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
    [MinLength(9, ErrorMessage = "Phone number must be 9 digits")]
    [MaxLength(9, ErrorMessage = "Phone number must be 9 digits")]
    public required string PhoneNumber { get; set; }
}