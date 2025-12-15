using BonProf.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers;

/// <summary>
/// Contrôleur pour la gestion des produits
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
[Route("[controller]")]
[ApiController]
[EnableCors]
public class ProductController(ProductService productService) : ControllerBase
{
    /// <summary>
    /// Récupère tous les produits
    /// </summary>
    /// <returns>Liste de tous les produits</returns>
    /// <response code="200">Produits récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Response<List<ProductDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<ProductDetails>>>> GetAllProducts()
    {
        var response = await productService.GetAllProductsAsync();

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant unique du produit</param>
    /// <returns>Produit trouvé</returns>
    /// <response code="200">Produit récupéré avec succès</response>
    /// <response code="404">Produit non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Response<ProductDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<ProductDetails>>> GetProductById(
        [FromRoute] Guid id)
    {
        var response = await productService.GetProductByIdAsync(id);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère tous les produits d'un cursus
    /// </summary>
    /// <param name="id">Identifiant du cursus</param>
    /// <returns>Liste des produits du cursus</returns>
    /// <response code="200">Produits du cursus récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("cursus/{id:guid}")]
    [ProducesResponseType(typeof(Response<List<ProductDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<ProductDetails>>>> GetProductsByCursusId(
        [FromRoute] Guid id)
    {
        var response = await productService.GetProductsByCursusIdAsync(id);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Récupère tous les produits d'un cursus
    /// </summary>
    /// <returns>Liste des produits du cursus</returns>
    /// <response code="200">Produits du cursus récupérés avec succès</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpGet("teacher/{id:guid}")]
    [ProducesResponseType(typeof(Response<List<ProductDetails>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<List<ProductDetails>>>> GetProductsByTeacher(
        [FromRoute] Guid id)
    {
        var response = await productService.GetProductsByTeacherIdAsync(id);

        if (response.Status == 200)
        {
            return Ok(response);
        }

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Crée un nouveau produit
    /// </summary>
    /// <param name="productDto">Données du produit à créer</param>
    /// <returns>Produit créé</returns>
    /// <response code="201">Produit créé avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="404">Cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<ProductDetails>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<ProductDetails>>> CreateProduct(
        [FromBody] ProductCreate productDto)
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

        var response = await productService.CreateProductAsync(productDto, User);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Met à jour un produit existant
    /// </summary>
    /// <param name="id">Identifiant du produit à mettre à jour</param>
    /// <param name="productDto">Nouvelles données du produit</param>
    /// <returns>Produit mis à jour</returns>
    /// <response code="200">Produit mis à jour avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="404">Produit ou cursus non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpPut]
    [ProducesResponseType(typeof(Response<ProductDetails>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<ProductDetails>>> UpdateProduct(
        [FromBody] ProductUpdate productDto)
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

        var response = await productService.UpdateProductAsync(productDto);

        return StatusCode(response.Status, response);
    }

    /// <summary>
    /// Supprime un produit (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du produit à supprimer</param>
    /// <returns>Résultat de l'opération de suppression</returns>
    /// <response code="200">Produit supprimé avec succès</response>
    /// <response code="404">Produit non trouvé</response>
    /// <response code="500">Erreur interne du serveur</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response<object>>> DeleteProduct(
        [FromRoute] Guid id)
    {
        var response = await productService.DeleteProductAsync(id);

        return StatusCode(response.Status, response);
    }
}
