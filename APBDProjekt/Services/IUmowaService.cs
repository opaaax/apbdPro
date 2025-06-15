using APBDProjekt.DTOs;
using APBDProjekt.Models;

namespace APBDProjekt.Services;

public interface IUmowaService
{
    Task<Umowa> AddUmowaAsync(AddUmowaDto umowa);
    Task<Platnosc> AddPlatnoscAsync(AddPlatnoscDto platnosc);
    Task UpdateAllPlatnosciAsync(int umowaId);
}