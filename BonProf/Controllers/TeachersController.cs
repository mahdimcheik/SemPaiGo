using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contr�leur pour la gestion des profils enseignants
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class TeachersController : ControllerBase
{
    private readonly TeacherService _teacherProfileService;

    public TeachersController(TeacherService teacherProfileService)
    {
        _teacherProfileService = teacherProfileService;
    }

    /// <summary>
    /// R�cup�re tous les profils enseignants
    /// </summary>
    /// <returns>Liste de tous les profils enseignants</returns>
    /// <response code="200">Profils r�cup�r�s avec succ�s</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<TeacherDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<UserDetails>>>> GetAllTeacherProfiles()
    {
        var response = await _teacherProfileService.GetAllTeacherProfilesAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// R�cup�re un profil enseignant par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du profil enseignant</param>
    /// <returns>Profil enseignant trouv�</returns>
    /// <response code="200">Profil r�cup�r� avec succ�s</response>
    /// <response code="404">Profil non trouv�</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("my-profile")]
    [Authorize]
    [ProducesResponseType(typeof(Response<UserDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UserDetails>>> GetMyTeacherProfile()
    {
        var response = await _teacherProfileService.GetTeacherFullProfileAsync( User);
        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// R�cup�re un profil enseignant par l'identifiant de l'utilisateur
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur</param>
    /// <returns>Profil enseignant de l'utilisateur</returns>
    /// <response code="200">Profil r�cup�r� avec succ�s</response>
    /// <response code="404">Profil non trouv�</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(Response<TeacherDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UserDetails>>> GetTeacherProfileByUserId(
        [FromRoute] Guid userId)
    {
        var response = await _teacherProfileService.GetTeacherProfileByUserIdAsync(userId);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met � jour le profil de l'enseignant connect�
    /// </summary>
    /// <param name="teacherUpdateDto">Donn�es de mise � jour du profil</param>
    /// <returns>Profil enseignant mis � jour</returns>
    /// <response code="200">Profil mis � jour avec succ�s</response>
    /// <response code="400">Donn�es invalides</response>
    /// <response code="401">Utilisateur non authentifi�</response>
    /// <response code="404">Profil enseignant non trouv�</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut("update-profile")]
    [Authorize(Roles = "Teacher")]
    [ProducesResponseType(typeof(Response<UserDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UserDetails>>> UpdateTeacherProfile(
        [FromBody] UserUpdate teacherUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response<object>
            {
                Status = 400,
                Message = "Donn�es de validation invalides",
                Data = ModelState
            });
        }

        var response = await _teacherProfileService.UpdateTeacherProfileAsync(teacherUpdateDto, User);

        return StatusCode(response.Status, response);
    }
}
