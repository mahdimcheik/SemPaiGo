using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des niveaux de cursus
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class LevelCursusController(LevelCursusService levelCursusService) : ControllerBase
{
    /// <summary>
    /// Récupère tous les niveaux de cursus
    /// </summary>
    /// <returns>Liste de tous les niveaux de cursus</returns>
    /// <response code="200">Niveaux de cursus récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<LevelCursusDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<LevelCursusDetails>>>> GetAllLevelCursus()
    {
        var response = await levelCursusService.GetAllLevelCursusAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un niveau de cursus par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du niveau de cursus</param>
    /// <returns>Niveau de cursus trouvé</returns>
    /// <response code="200">Niveau de cursus récupéré avec succès</response>
    /// <response code="404">Niveau de cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<LevelCursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<LevelCursusDetails>>> GetLevelCursusById(
        [FromRoute] Guid id)
    {
        var response = await levelCursusService.GetLevelCursusByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée un nouveau niveau de cursus
    /// </summary>
    /// <param name="levelDto">Données du niveau de cursus à créer</param>
    /// <returns>Niveau de cursus créé</returns>
    /// <response code="201">Niveau de cursus créé avec succès</response>
    /// <response code="400">Données invalides ou niveau de cursus existant</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<LevelCursusDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<LevelCursusDetails>>> CreateLevelCursus(
        [FromBody] LevelCursusCreate levelDto)
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

        var response = await levelCursusService.CreateLevelCursusAsync(levelDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour un niveau de cursus existant
    /// </summary>
    /// <param name="id">Identifiant du niveau de cursus à mettre à jour</param>
    /// <param name="levelDto">Nouvelles données du niveau de cursus</param>
    /// <returns>Niveau de cursus mis à jour</returns>
    /// <response code="200">Niveau de cursus mis à jour avec succès</response>
    /// <response code="400">Données invalides ou nom de niveau de cursus déjà utilisé</response>
    /// <response code="404">Niveau de cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut]
    [ProducesResponseType(typeof(Response<LevelCursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<LevelCursusDetails>>> UpdateLevelCursus( [FromBody] LevelCursusUpdate levelDto)
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

        var response = await levelCursusService.UpdateLevelCursusAsync( levelDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un niveau de cursus (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du niveau de cursus à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Niveau de cursus supprimé avec succès</response>
    /// <response code="400">Niveau de cursus utilisé par des cursus</response>
    /// <response code="404">Niveau de cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<object>>> DeleteLevelCursus(
        [FromRoute] Guid id)
    {
        var response = await levelCursusService.DeleteLevelCursusAsync(id);

        return StatusCode(response.Status, response);
    }
}
