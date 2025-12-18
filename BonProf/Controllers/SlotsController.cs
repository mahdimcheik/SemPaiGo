using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des créneaux
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class SlotsController(SlotsService slotsService) : ControllerBase
{
    /// <summary>
    /// Ajoute un nouveau créneau pour l'enseignant connecté
    /// </summary>
    /// <param name="slotDto">Données du créneau à créer</param>
    /// <returns>Créneau créé</returns>
    /// <response code="201">Créneau créé avec succès</response>
    /// <response code="400">Données invalides ou chevauchement de créneaux</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="403">Vous devez être un enseignant</response>
    /// <response code="404">Type de créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [Authorize(Roles = "Teacher")]
    [HttpPost("teacher/add")]
    [ProducesResponseType(typeof(Response<SlotDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SlotDetails>>> AddSlotByTeacher(
        [FromBody] SlotCreate slotDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new Response<object>
                {
                    Status = 400,
                    Message = "Données de validation invalides",
                    Data = ModelState,
                }
            );
        }

        var response = await slotsService.AddSlotByTeacherAsync(slotDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Ajoute un nouveau créneau pour l'enseignant connecté
    /// </summary>
    /// <param name="slotDto">Données du créneau à créer</param>
    /// <returns>Créneau créé</returns>
    /// <response code="201">Créneau créé avec succès</response>
    /// <response code="400">Données invalides ou chevauchement de créneaux</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="403">Vous devez être un enseignant</response>
    /// <response code="404">Type de créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [Authorize(Roles = "Teacher")]
    [HttpPut("teacher/update")]
    [ProducesResponseType(typeof(Response<SlotDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<SlotDetails>>> UpdateSlotByTeacher(
        [FromBody] SlotUpdate slotDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new Response<object>
                {
                    Status = 400,
                    Message = "Données de validation invalides",
                    Data = ModelState,
                }
            );
        }

        var response = await slotsService.UpdateSlotByTeacherAsync(slotDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un créneau de l'enseignant connecté (suppression logique)
    /// </summary>
    /// <param name="slotId">Identifiant du créneau à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Créneau supprimé avec succès</response>
    /// <response code="400">Créneau déjà réservé</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="403">Vous n'êtes pas autorisé à supprimer ce créneau</response>
    /// <response code="404">Créneau non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [Authorize(Roles = "Teacher")]
    [HttpDelete("teacher/remove/{slotId:guid}")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<bool>>> RemoveSlotByTeacher(
        [FromRoute] Guid slotId
    )
    {
        var response = await slotsService.RemoveSlotByTeacherAsync(slotId, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère les créneaux de l'enseignant connecté entre deux dates
    /// </summary>
    /// <param name="dateFrom">Date de début (format ISO 8601)</param>
    /// <param name="dateTo">Date de fin (format ISO 8601)</param>
    /// <returns>Liste des créneaux</returns>
    /// <response code="200">Créneaux récupérés avec succès</response>
    /// <response code="400">Dates invalides</response>
    /// <response code="401">Utilisateur non authentifié</response>
    /// <response code="403">Vous devez être un enseignant</response>
    /// <response code="500">Erreur interne du serveur</response>
    [Authorize(Roles = "Teacher")]
    [HttpGet("teacher/my-slots")]
    [ProducesResponseType(typeof(Response<List<SlotDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<SlotDetails>>>> GetSlotsByTeacher(
        [FromQuery] DateTimeOffset dateFrom,
        [FromQuery] DateTimeOffset dateTo
    )
    {
        var response = await slotsService.GetSlotsByTeacherAndDatesAsync(User, dateFrom, dateTo);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère les créneaux disponibles d'un enseignant entre deux dates (consultation publique)
    /// </summary>
    /// <param name="teacherId">Identifiant de l'enseignant</param>
    /// <param name="dateFrom">Date de début (format ISO 8601)</param>
    /// <param name="dateTo">Date de fin (format ISO 8601)</param>
    /// <returns>Liste des créneaux disponibles</returns>
    /// <response code="200">Créneaux disponibles récupérés avec succès</response>
    /// <response code="400">Dates invalides</response>
    /// <response code="404">Enseignant non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [AllowAnonymous]
    [HttpGet("teacher/{teacherId:guid}/available-slots")]
    [ProducesResponseType(typeof(Response<List<SlotDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<SlotDetails>>>> GetAvailableSlotsByTeacher(
        [FromRoute] Guid teacherId,
        [FromQuery] DateTimeOffset dateFrom,
        [FromQuery] DateTimeOffset dateTo
    )
    {
        var response = await slotsService.GetSlotsByTeacherIdAndDatesAsync(
            teacherId,
            dateFrom,
            dateTo
        );

        return StatusCode(response.Status, response);
    }
}
