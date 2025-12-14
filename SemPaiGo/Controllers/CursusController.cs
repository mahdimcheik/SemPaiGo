using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des cursus
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class CursusController(CursusService cursusService, MainContext context) : ControllerBase
{
    /// <summary>
    /// Récupère tous les cursus
    /// </summary>
    /// <returns>Liste de tous les cursus</returns>
    /// <response code="200">Cursus récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<CursusDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<CursusDetails>>>> GetAllCursus()
    {
        var response = await cursusService.GetAllCursusAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un cursus par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du cursus</param>
    /// <returns>Cursus trouvé</returns>
    /// <response code="200">Cursus récupéré avec succès</response>
    /// <response code="404">Cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<CursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CursusDetails>>> GetCursusById(
        [FromRoute] Guid id)
    {
        var response = await cursusService.GetCursusByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère tous les cursus d'un enseignant
    /// </summary>
    /// <param name="id">Identifiant de l'enseignant</param>
    /// <returns>Liste des cursus de l'enseignant</returns>
    /// <response code="200">Cursus de l'enseignant récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("teacher/{id:guid}")]
    [ProducesResponseType(typeof(Response<List<CursusDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<CursusDetails>>>> GetCursusByTeacherId(
        [FromRoute] Guid id)
    {
        var response = await cursusService.GetCursusByTeacherIdAsync(id);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère tous les cursus d'un niveau
    /// </summary>
    /// <param name="id">Identifiant du niveau</param>
    /// <returns>Liste des cursus du niveau</returns>
    /// <response code="200">Cursus du niveau récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("level/{id:guid}")]
    [ProducesResponseType(typeof(Response<List<CursusDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<CursusDetails>>>> GetCursusByLevelId(
        [FromRoute] Guid id)
    {
        var response = await cursusService.GetCursusByLevelIdAsync(id);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée un nouveau cursus
    /// </summary>
    /// <param name="cursusDto">Données du cursus à créer</param>
    /// <returns>Cursus créé</returns>
    /// <response code="201">Cursus créé avec succès</response>
    /// <response code="400">Données invalides ou cursus existant</response>
    /// <response code="404">Niveau, enseignant ou catégorie non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost("create")]
    [ProducesResponseType(typeof(Response<CursusDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CursusDetails>>> CreateCursus(
        [FromBody] CursusCreate cursusDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response<object>
            {
                Status = 400,
                Message = "Données de validation invalides",
                Data = ModelState
            });
        }

        var response = await cursusService.CreateCursusAsync(cursusDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour un cursus existant
    /// </summary>
    /// <param name="id">Identifiant du cursus à mettre à jour</param>
    /// <param name="cursusDto">Nouvelles données du cursus</param>
    /// <returns>Cursus mis à jour</returns>
    /// <response code="200">Cursus mis à jour avec succès</response>
    /// <response code="400">Données invalides ou nom de cursus déjà utilisé</response>
    /// <response code="404">Cursus, niveau, enseignant ou catégorie non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut("update/{id:guid}")]
    [ProducesResponseType(typeof(Response<CursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CursusDetails>>> UpdateCursus(
        [FromRoute] Guid id,
        [FromBody] CursusUpdate cursusDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response<object>
            {
                Status = 400,
                Message = "Données de validation invalides",
                Data = ModelState
            });
        }

        var response = await cursusService.UpdateCursusAsync(cursusDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un cursus (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du cursus à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Cursus supprimé avec succès</response>
    /// <response code="404">Cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<object>>> DeleteCursus(
        [FromRoute] Guid id)
    {
        var response = await cursusService.DeleteCursusAsync(id);

        return StatusCode(response.Status, response);
    }   
}
