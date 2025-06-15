using APBDProjekt.DTOs;

namespace APBDProjekt.Services;

public interface IPrzychodService
{
    Task<GetPrzychodDto> GetPrzychodAsync(int? productId, string? waluta, bool czyPrzewidywany);
}