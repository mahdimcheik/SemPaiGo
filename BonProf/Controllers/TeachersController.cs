using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des profils enseignants
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
    /// Récupère tous les profils enseignants
    /// </summary>
    /// <returns>Liste de tous les profils enseignants</returns>
    /// <response code="200">Profils récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<TeacherDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<TeacherDetails>>>> GetAllTeacherProfiles()
    {
        var response = await _teacherProfileService.GetAllTeacherProfilesAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un profil enseignant par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du profil enseignant</param>
    /// <returns>Profil enseignant trouvé</returns>
    /// <response code="200">Profil récupéré avec succès</response>
    /// <response code="404">Profil non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("my-profile")]
    [Authorize]
    [ProducesResponseType(typeof(Response<UserDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<UserDetails>>> GetTeacherProfileById()
    {
        var response = await _teacherProfileService.GetTeacherFullProfileAsync( User);
        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un profil enseignant par l'identifiant de l'utilisateur
    /// </summary>
    /// <param name="userId">Identifiant de l'utilisateur</param>
    /// <returns>Profil enseignant de l'utilisateur</returns>
    /// <response code="200">Profil récupéré avec succès</response>
    /// <response code="404">Profil non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(Response<TeacherDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<TeacherDetails>>> GetTeacherProfileByUserId(
        [FromRoute] Guid userId)
    {
        var response = await _teacherProfileService.GetTeacherProfileByUserIdAsync(userId);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }   
}
