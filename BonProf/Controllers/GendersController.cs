using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des genres
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class GendersController(GendersService gendersService) : ControllerBase
{
    /// <summary>
    /// Récupère tous les genres
    /// </summary>
    /// <returns>Liste de tous les genres</returns>
    /// <response code="200">Genres récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<GenderDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<GenderDetails>>>> GetAllGenders()
    {
        var response = await gendersService.GetAllGendersAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }
}
