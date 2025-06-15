using APBDProjekt.Data;
using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Services;

public class ClientService : IClientService
{
    
    private readonly Context _context;
    public ClientService(Context context)
    {
        _context = context;
    }
    
    public async Task AddClientAsync(AddClientDto client)
    {
        
        if (!string.IsNullOrEmpty(client.Pesel))
        {
            var osobaFizyczna = new OsobaFizyczna
            {
                Imie = client.Imie ?? throw new ArgumentException("Imie is required for OsobaFizyczna"),
                Nazwisko = client.Nazwisko ?? throw new ArgumentException("Nazwisko is required for OsobaFizyczna"),
                Pesel = client.Pesel,
                Adres = client.Adres,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                IsDeleted = false
            };
        
            await _context.Clients.AddAsync(osobaFizyczna);
        }
        else if (!string.IsNullOrEmpty(client.Krs))
        {
            var firma = new Firma
            {
                Nazwa = client.Nazwa ?? throw new ArgumentException("Nazwa is required for Firma"),
                Krs = client.Krs,
                Adres = client.Adres,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
        
            await _context.Clients.AddAsync(firma);
        }
        else
        {
            throw new ArgumentException("Invalid client data: Must provide either Pesel or Krs");
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        
        if (client == null)
        {
            throw new NotFoundException("Client not found.");
        }
        
        var firma = await _context.Firmy.FirstOrDefaultAsync(f => f.Id == id);

        if (firma != null)
        {
            throw new BadRequestException("Nie można usunąć firmy");
        }
        
        var osobaFizyczna = await _context.OsobaFizyczne.FirstOrDefaultAsync(f => f.Id == id);
        if (osobaFizyczna == null)
        {
            throw new Exception("Database error.");
        }
        
        osobaFizyczna.IsDeleted = true;
        await _context.SaveChangesAsync();
        
    }

    public async Task<Client> UpdateClientAsync(int id, UpdateClientDto client)
    {
        var clientToUpdate = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

        if (clientToUpdate == null)
        {
            throw new NotFoundException("Client not found.");
        }

        foreach (var property in client.GetType().GetProperties())
        {
            var value = property.GetValue(client);
            if (value != null)
            {
                property.SetValue(clientToUpdate, value);
            }
        }

        await _context.SaveChangesAsync();
        
        return clientToUpdate;
    }

    public async Task<List<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }
}