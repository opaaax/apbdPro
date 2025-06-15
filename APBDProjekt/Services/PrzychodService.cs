using APBDProjekt.Data;
using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Tools;

namespace APBDProjekt.Services;

public class PrzychodService : IPrzychodService
{
    private readonly Context _context;
    private readonly IWalutaProcessor _walutaProcessor;
    
    public PrzychodService(Context context, IWalutaProcessor walutaProcessor)
    {
        _context = context;
        _walutaProcessor = walutaProcessor;
    }

    public async Task<GetPrzychodDto> GetPrzychodAsync(int? productId, string? waluta, bool czyPrzewidywany = false)
    {
        decimal przychod;
        if (productId == null)
        {
            przychod = _context.Umowy
                .Where(umowa => czyPrzewidywany || umowa.CzyPodpisana == true)
                .Select(umowa => umowa.FinalnaCena)
                .Sum();
        }
        else
        {
            var product = _context.Oprogramowania.FirstOrDefault(o => o.Id == productId);
            if (product == null)
            {
                throw new NotFoundException("Nie znaleziono produktu");
            }
            
            przychod = _context.Umowy
                .Where(umowa => (czyPrzewidywany || umowa.CzyPodpisana == true) && umowa.OprogramowanieId == productId)
                .Select(umowa => umowa.FinalnaCena)
                .Sum();
        }

        if (waluta != null)
        {
            przychod = await _walutaProcessor.ProcessCurrency(przychod, waluta);
        }
        
        return new GetPrzychodDto()
        {
            Przychod = przychod
        };
    }
}