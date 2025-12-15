using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des formations
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class FormationsController(FormationsService formationsService) : ControllerBase
{
    /// <summary>
    /// Récupère toutes les formations
    /// </summary>
    /// <returns>Liste de toutes les formations</returns>
    /// <response code="200">Formations récupérées avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<FormationDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<FormationDetails>>>> GetAllFormations()
    {
        var response = await formationsService.GetAllFormationsAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère une formation par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique de la formation</param>
    /// <returns>Formation trouvée</returns>
    /// <response code="200">Formation récupérée avec succès</response>
    /// <response code="404">Formation non trouvée</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<FormationDetails>>> GetFormationById(
        [FromRoute] Guid id)
    {
        var response = await formationsService.GetFormationByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère toutes les formations d'un utilisateur
    /// </summary>
    /// <param name="id">Identifiant de l'utilisateur</param>
    /// <returns>Liste des formations de l'utilisateur</returns>
    /// <response code="200">Formations de l'utilisateur récupérées avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("teacher/{id:guid}")]
    [ProducesResponseType(typeof(Response<List<FormationDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<FormationDetails>>>> GetFormationsByUserId(
        [FromRoute] Guid id)
    {
        var response = await formationsService.GetFormationsByUserIdAsync(id);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée une nouvelle formation
    /// </summary>
    /// <param name="formationDto">Données de la formation à créer</param>
    /// <returns>Formation créée</returns>
    /// <response code="201">Formation créée avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="404">Utilisateur non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<FormationDetails>>> CreateFormation(
        [FromBody] FormationCreate formationDto)
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

        var response = await formationsService.CreateFormationAsync(formationDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour une formation existante
    /// </summary>
    /// <param name="id">Identifiant de la formation à mettre à jour</param>
    /// <param name="formationDto">Nouvelles données de la formation</param>
    /// <returns>Formation mise à jour</returns>
    /// <response code="200">Formation mise à jour avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="404">Formation ou utilisateur non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<FormationDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<FormationDetails>>> UpdateFormation(
        [FromBody] FormationUpdate formationDto)
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

        var response = await formationsService.UpdateFormationAsync( formationDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime une formation (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant de la formation à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Formation supprimée avec succès</response>
    /// <response code="404">Formation non trouvée</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<bool>>> DeleteFormation(
        [FromRoute] Guid id)
    {
        var response = await formationsService.DeleteFormationAsync(id);

        return StatusCode(response.Status, response);
    }
}
