using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;


/// <summary>
/// Contrôleur pour la gestion des catégories de cursus
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class CategoryCursusController(CategoryCursusService categoryCursusService) : ControllerBase
{
    /// <summary>
    /// Récupère toutes les catégories de cursus
    /// </summary>
    /// <returns>Liste de toutes les catégories de cursus</returns>
    /// <response code="200">Catégories de cursus récupérées avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<CategoryCursusDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<CategoryCursusDetails>>>> GetAllCategoryCursus()
    {
        var response = await categoryCursusService.GetAllCategoryCursusAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère une catégorie de cursus par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique de la catégorie de cursus</param>
    /// <returns>Catégorie de cursus trouvée</returns>
    /// <response code="200">Catégorie de cursus récupérée avec succès</response>
    /// <response code="404">Catégorie de cursus non trouvée</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<CategoryCursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CategoryCursusDetails>>> GetCategoryCursusById(
        [FromRoute] Guid id)
    {
        var response = await categoryCursusService.GetCategoryCursusByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée une nouvelle catégorie de cursus
    /// </summary>
    /// <param name="categoryDto">Données de la catégorie de cursus à créer</param>
    /// <returns>Catégorie de cursus créée</returns>
    /// <response code="201">Catégorie de cursus créée avec succès</response>
    /// <response code="400">Données invalides ou catégorie de cursus existante</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<CategoryCursusDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CategoryCursusDetails>>> CreateCategoryCursus(
        [FromBody] CategoryCursusCreate categoryDto)
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

        var response = await categoryCursusService.CreateCategoryCursusAsync(categoryDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour une catégorie de cursus existante
    /// </summary>
    /// <param name="id">Identifiant de la catégorie de cursus à mettre à jour</param>
    /// <param name="categoryDto">Nouvelles données de la catégorie de cursus</param>
    /// <returns>Catégorie de cursus mise à jour</returns>
    /// <response code="200">Catégorie de cursus mise à jour avec succès</response>
    /// <response code="400">Données invalides ou nom de catégorie de cursus déjà utilisé</response>
    /// <response code="404">Catégorie de cursus non trouvée</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Response<CategoryCursusDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<CategoryCursusDetails>>> UpdateCategoryCursus(
        [FromRoute] Guid id,
        [FromBody] CategoryCursusUpdate categoryDto)
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

        var response = await categoryCursusService.UpdateCategoryCursusAsync(id, categoryDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime une catégorie de cursus (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant de la catégorie de cursus à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Catégorie de cursus supprimée avec succès</response>
    /// <response code="400">Catégorie de cursus utilisée par des cursus</response>
    /// <response code="404">Catégorie de cursus non trouvée</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<object>>> DeleteCategoryCursus(
        [FromRoute] Guid id)
    {
        var response = await categoryCursusService.DeleteCategoryCursusAsync(id);

        return StatusCode(response.Status, response);
    }
}