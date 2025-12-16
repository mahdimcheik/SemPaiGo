using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des types de créneaux
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class TypeSlotsController(TypeSlotsService typeSlotsService) : ControllerBase
{
    /// <summary>
    /// Récupère tous les types de créneaux
    /// </summary>
    /// <returns>Liste de tous les types de créneaux</returns>
    /// <response code="200">Types de créneaux récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<TypeSlotDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<TypeSlotDetails>>>> GetAllTypeSlots()
    {
        var response = await typeSlotsService.GetAllTypeSlotsAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un type de créneau par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du type de créneau</param>
    /// <returns>Type de créneau trouvé</returns>
    /// <response code="200">Type de créneau récupéré avec succès</response>
    /// <response code="404">Type de créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<TypeSlotDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<TypeSlotDetails>>> GetTypeSlotById(
        [FromRoute] Guid id)
    {
        var response = await typeSlotsService.GetTypeSlotByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée un nouveau type de créneau
    /// </summary>
    /// <param name="typeSlotDto">Données du type de créneau à créer</param>
    /// <returns>Type de créneau créé</returns>
    /// <response code="201">Type de créneau créé avec succès</response>
    /// <response code="400">Données invalides ou type de créneau existant</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<TypeSlotDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<TypeSlotDetails>>> CreateTypeSlot(
        [FromBody] TypeSlotCreate typeSlotDto)
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

        var response = await typeSlotsService.CreateTypeSlotAsync(typeSlotDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour un type de créneau existant
    /// </summary>
    /// <param name="id">Identifiant du type de créneau à mettre à jour</param>
    /// <param name="typeSlotDto">Nouvelles données du type de créneau</param>
    /// <returns>Type de créneau mis à jour</returns>
    /// <response code="200">Type de créneau mis à jour avec succès</response>
    /// <response code="400">Données invalides ou nom de type de créneau déjà utilisé</response>
    /// <response code="404">Type de créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut]
    [ProducesResponseType(typeof(Response<TypeSlotDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<TypeSlotDetails>>> UpdateTypeSlot(
        [FromBody] TypeSlotUpdate typeSlotDto)
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

        var response = await typeSlotsService.UpdateTypeSlotAsync( typeSlotDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un type de créneau (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du type de créneau à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Type de créneau supprimé avec succès</response>
    /// <response code="404">Type de créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<bool>>> DeleteTypeSlot(
        [FromRoute] Guid id)
    {
        var response = await typeSlotsService.DeleteTypeSlotAsync(id);

        return StatusCode(response.Status, response);
    }
}
