using APBDProjekt.Exceptions;
using APBDProjekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBDProjekt.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin")]
public class PrzychodController : ControllerBase
{
    private readonly IPrzychodService _przychodService;

    public PrzychodController(IPrzychodService przychodService)
    {
        _przychodService = przychodService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPrzychodAsync(int? productId, string? waluta, bool czyPrzewidywany = false)
    {
        try
        {
            var przychod = await _przychodService.GetPrzychodAsync(productId, waluta, czyPrzewidywany);
            return Ok(przychod);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    
}