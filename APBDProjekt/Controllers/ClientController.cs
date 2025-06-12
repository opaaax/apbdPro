using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBDProjekt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientsAsync()
    {
        return Ok(await _clientService.GetAllClientsAsync());
    }

    public async Task<IActionResult> AddClientAsync(AddClientDto client)
    {
        try
        {
            await _clientService.AddClientAsync(client);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        try
        {
            await _clientService.DeleteClientAsync(id);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    public async Task<IActionResult> UpdateClientAsync(int id, UpdateClientDto client)
    {
        try
        {
            var returnClient = await _clientService.UpdateClientAsync(id, client);
            return Ok(returnClient);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}