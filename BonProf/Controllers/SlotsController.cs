using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contr�leur pour la gestion des cr�neaux
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class SlotsController(SlotsService slotsService) : ControllerBase
{
    /// <summary>
    /// Ajoute un nouveau cr�neau pour l'enseignant connect�
    /// </summary>
    /// <param name="slotDto">Donn�es du cr�neau � cr�er</param>
    /// <returns>Cr�neau cr��</returns>
    /// <response code="201">Cr�neau cr�� avec succ�s</response>
    /// <response code="400">Donn�es invalides ou chevauchement de cr�neaux</response>
    /// <response code="401">Utilisateur non authentifi�</response>
    /// <response code="403">Vous devez �tre un enseignant</response>
    /// <response code="404">Type de cr�neau non trouv�</response>
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
                    Message = "Donn�es de validation invalides",
                    Data = ModelState,
                }
            );
        }

        var response = await slotsService.AddSlotByTeacherAsync(slotDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Ajoute un nouveau cr�neau pour l'enseignant connect�
    /// </summary>
    /// <param name="slotDto">Donn�es du cr�neau � cr�er</param>
    /// <returns>Cr�neau cr��</returns>
    /// <response code="201">Cr�neau cr�� avec succ�s</response>
    /// <response code="400">Donn�es invalides ou chevauchement de cr�neaux</response>
    /// <response code="401">Utilisateur non authentifi�</response>
    /// <response code="403">Vous devez �tre un enseignant</response>
    /// <response code="404">Type de cr�neau non trouv�</response>
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
                    Message = "Donn�es de validation invalides",
                    Data = ModelState,
                }
            );
        }

        var response = await slotsService.UpdateSlotByTeacherAsync(slotDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un cr�neau de l'enseignant connect� (suppression logique)
    /// </summary>
    /// <param name="slotId">Identifiant du cr�neau � supprimer</param>
    /// <returns>R�sultat de l'op�ration de suppression</returns>
    /// <response code="200">Cr�neau supprim� avec succ�s</response>
    /// <response code="400">Cr�neau d�j� r�serv�</response>
    /// <response code="401">Utilisateur non authentifi�</response>
    /// <response code="403">Vous n'�tes pas autoris� � supprimer ce cr�neau</response>
    /// <response code="404">Cr�neau non trouv�</response>
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
    /// R�cup�re les cr�neaux de l'enseignant connect� entre deux dates
    /// </summary>
    /// <param name="periodTime">Date de d�but et de fin (format ISO 8601)</param>
    /// <returns>Liste des cr�neaux</returns>
    /// <response code="200">Cr�neaux r�cup�r�s avec succ�s</response>
    /// <response code="400">Dates invalides</response>
    /// <response code="401">Utilisateur non authentifi�</response>
    /// <response code="403">Vous devez �tre un enseignant</response>
    /// <response code="500">Erreur interne du serveur</response>
    [Authorize(Roles = "Teacher")]
    [HttpPost("teacher/my-slots")]
    [ProducesResponseType(typeof(Response<List<SlotDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<SlotDetails>>>> GetSlotsByTeacher(
        [FromBody] PeriodTime periodTime
    )
    {
        var response = await slotsService.GetSlotsByTeacherAndDatesAsync(User, periodTime.DateFrom, periodTime.DateTo);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// R�cup�re les cr�neaux disponibles d'un enseignant entre deux dates (consultation publique)
    /// </summary>
    /// <param name="teacherId">Identifiant de l'enseignant</param>
    /// <param name="dateFrom">Date de d�but (format ISO 8601)</param>
    /// <param name="dateTo">Date de fin (format ISO 8601)</param>
    /// <returns>Liste des cr�neaux disponibles</returns>
    /// <response code="200">Cr�neaux disponibles r�cup�r�s avec succ�s</response>
    /// <response code="400">Dates invalides</response>
    /// <response code="404">Enseignant non trouv�</response>
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
