using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des rôles
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class RolesController(RolesService rolesService) : ControllerBase
{
    /// <summary>
    /// Récupère tous les rôles
    /// </summary>
    /// <returns>Liste de tous les rôles</returns>
    /// <response code="200">Rôles récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<RoleDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<RoleDetails>>>> GetAllRoles()
    {
        var response = await rolesService.GetAllRolesAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }
}
