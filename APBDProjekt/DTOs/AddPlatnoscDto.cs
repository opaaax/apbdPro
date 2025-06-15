using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.DTOs;

public class AddPlatnoscDto
{ 
    [Required]
    public int UmowaId { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    [Precision(10,2)]
    public decimal Kwota { get; set; }
}