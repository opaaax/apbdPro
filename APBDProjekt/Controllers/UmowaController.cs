using System.ComponentModel.DataAnnotations;
using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBDProjekt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UmowaController : ControllerBase
{
    private readonly IUmowaService _umowaService;

    public UmowaController(IUmowaService umowaService)
    {
        _umowaService = umowaService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUmowaAsync(AddUmowaDto umowa)
    {
        try
        {
            var created = await _umowaService.AddUmowaAsync(umowa);
            return Created("", created);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("platnosc")]
    public async Task<IActionResult> AddPlatnoscAsync(AddPlatnoscDto platnosc)
    {
        try
        {
            var created = await _umowaService.AddPlatnoscAsync(platnosc);
            return Created("", created);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }
}