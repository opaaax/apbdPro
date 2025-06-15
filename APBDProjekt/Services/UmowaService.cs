using APBDProjekt.Data;
using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Services;

public class UmowaService : IUmowaService
{
    private readonly Context _context;
    public UmowaService(Context context)
    {
        _context = context;
    }
    
    public async Task<Umowa> AddUmowaAsync(AddUmowaDto umowa)
    {
        var isActive = !(umowa.DataZakonczenia >= DateTime.Now);
        
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == umowa.ClientId);
        var oprogramowanie = await _context.Oprogramowania
            .Include(oprogramowanie => oprogramowanie.Znizki)
            .FirstOrDefaultAsync(o => o.Id == umowa.OprogramowanieId);

        if (client == null)
        {
            throw new NotFoundException("Client not found.");
        }

        if (oprogramowanie == null)
        {
            throw new NotFoundException("Oprogramowanie not found.");
        }
        
        var czyJuzMaLicencje = await _context.Licencje.AnyAsync(
            l => l.ClientId == umowa.ClientId 
                 && l.OprogramowanieId == umowa.OprogramowanieId 
                 && l.Oprogramowanie.InfoOWersji == oprogramowanie.InfoOWersji
                 );
        
        
        var znizki = oprogramowanie.Znizki.ToList();
        var czyPoprzedniKlient =
            await _context.Umowy.AnyAsync(u => u.ClientId == umowa.ClientId && u.CzyPodpisana == true) 
            || await _context.Licencje.AnyAsync(l => l.ClientId == umowa.ClientId);

        var finalPrice = oprogramowanie.CenaZaRok
                         * (znizki.Select(e => e.WartoscWProcentach).Max() / 100)
                         * (czyPoprzedniKlient ? 1 : (decimal) 0.95)
                         + 1000 * umowa.DodatkoweLataAktualizacji;
        
        var umowaToAdd = new Umowa()
        {
            ClientId = umowa.ClientId,
            OprogramowanieId = umowa.OprogramowanieId,
            DataRozpoczecia = umowa.DataRozpoczecia,
            DataZakonczenia = umowa.DataZakonczenia,
            CzyAktywna = isActive,
            FinalnaCena = finalPrice,
            InfoOAktualizacjach = umowa.InfoOAktualizacjach,
            DodatkoweLataAktualizacji = umowa.DodatkoweLataAktualizacji,
            WersjaOprogramowania = umowa.WersjaOprogramowania,
        };
        
        await _context.Umowy.AddAsync(umowaToAdd);
        await _context.SaveChangesAsync();
        
        return umowaToAdd;
    }

    public async Task<Platnosc> AddPlatnoscAsync(AddPlatnoscDto platnosc)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == platnosc.ClientId);
            if (client == null)
            {
                throw new NotFoundException("Client not found.");
            }

            var umowa = await _context.Umowy.FirstOrDefaultAsync(u => u.Id == platnosc.UmowaId);
            if (umowa == null)
            {
                throw new NotFoundException("Umowa not found.");
            }

            if (umowa.DataZakonczenia > DateTime.Now)
            {
                await UpdateAllPlatnosciAsync(umowa.Id);
                throw new PaymentTimeElapsedException("The payment deadline has elapsed");
            }

            var moneySum = await _context.Platnosci
                .Where(p => p.UmowaId == platnosc.UmowaId)
                .SumAsync(p => p.Kwota) + platnosc.Kwota;

            if (moneySum > umowa.FinalnaCena)
            {
                throw new BadRequestException("The sum of payments exceeds the final price of the contract.");
            }

            if (Math.Abs(moneySum - umowa.FinalnaCena) < (decimal)0.01)
            {
                umowa.CzyPodpisana = true;
                await _context.SaveChangesAsync();
            }

            var platnoscToAdd = new Platnosc()
            {
                ClientId = platnosc.ClientId,
                UmowaId = platnosc.UmowaId,
                Kwota = platnosc.Kwota
            };

            await _context.Platnosci.AddAsync(platnoscToAdd);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return platnoscToAdd;
        }
        catch (PaymentTimeElapsedException e)
        {
            await transaction.CommitAsync();
            throw;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task UpdateAllPlatnosciAsync(int umowaId)
    {
        var platnosci = _context.Platnosci.Where(p => p.UmowaId == umowaId).ToList();
        foreach (var platnosc in platnosci)
        {
            platnosc.CzyZwrocona = true;
            _context.Platnosci.Update(platnosc);
            _context.SaveChanges();
        }
        return Task.CompletedTask;
    }
}