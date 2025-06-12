using APBDProjekt.DTOs;
using APBDProjekt.Models;

namespace APBDProjekt.Services;

public interface IClientService
{
    Task AddClientAsync(AddClientDto client);
    Task DeleteClientAsync(int id);
    Task<Client> UpdateClientAsync(int id, UpdateClientDto client);
    Task<List<Client>> GetAllClientsAsync();
}