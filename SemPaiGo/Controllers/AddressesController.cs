using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Services;

namespace SemPaiGo.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des adresses
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class AddressesController(MainContext context, AddressesService addressesService) : ControllerBase
    {
        /// <summary>
        /// Récupère toutes les adresses
        /// </summary>
        /// <returns>Liste de toutes les adresses</returns>
        /// <response code="200">Adresses récupérées avec succès</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(Response<List<AddressDetails>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<List<AddressDetails>>>> GetAllAddresses()
        {
            //var query = context.Addresses.AsQueryable();
            //var res  = (IQueryable<Address>)options.ApplyTo(query);
            //res.ToList();
            var response = await addressesService.GetAllAddressesAsync();

            if (response.Status == 200)
            {
                return Ok(response);
            }

            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Récupère une adresse par son identifiant
        /// </summary>
        /// <param name="id">Identifiant unique de l'adresse</param>
        /// <returns>Adresse trouvée</returns>
        /// <response code="200">Adresse récupérée avec succès</response>
        /// <response code="404">Adresse non trouvée</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Response<AddressDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<AddressDetails>>> GetAddressById(
            [FromRoute] Guid id)
        {
            var response = await addressesService.GetAddressByIdAsync(id);

            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Récupère toutes les adresses d'un utilisateur
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur</param>
        /// <returns>Liste des adresses de l'utilisateur</returns>
        /// <response code="200">Adresses de l'utilisateur récupérées avec succès</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpGet("user/{id:guid}")]
        [ProducesResponseType(typeof(Response<List<AddressDetails>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<List<AddressDetails>>>> GetAddressesByUserId(
            [FromRoute] Guid id)
        {
            var response = await addressesService.GetAddressesByUserIdAsync(id);

            if (response.Status == 200)
            {
                return Ok(response);
            }

            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Crée une nouvelle adresse
        /// </summary>
        /// <param name="addressDto">Données de l'adresse à créer</param>
        /// <returns>Adresse créée</returns>
        /// <response code="201">Adresse créée avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="404">Utilisateur non trouvé</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpPost]
        [ProducesResponseType(typeof(Response<AddressDetails>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<AddressDetails>>> CreateAddress(
            [FromBody] AddressCreate addressDto)
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

            var response = await addressesService.CreateAddressAsync(addressDto, User);

            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Met à jour une adresse existante
        /// </summary>
        /// <param name="id">Identifiant de l'adresse à mettre à jour</param>
        /// <param name="addressDto">Nouvelles données de l'adresse</param>
        /// <returns>Adresse mise à jour</returns>
        /// <response code="200">Adresse mise à jour avec succès</response>
        /// <response code="400">Données invalides</response>
        /// <response code="404">Adresse ou utilisateur non trouvé</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpPut]
        [ProducesResponseType(typeof(Response<AddressDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<AddressDetails>>> UpdateAddress(
            [FromBody] AddressUpdate addressDto)
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

            var response = await addressesService.UpdateAddressAsync(addressDto);

            return StatusCode(response.Status, response);
        }

        /// <summary>
        /// Supprime une adresse (suppression logique)
        /// </summary>
        /// <param name="id">Identifiant de l'adresse à supprimer</param>
        /// <returns>Résultat de l'opération de suppression</returns>
        /// <response code="200">Adresse supprimée avec succès</response>
        /// <response code="404">Adresse non trouvée</response>
        /// <response code="500">Erreur interne du serveur</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<object>>> DeleteAddress(
            [FromRoute] Guid id)
        {
            var response = await addressesService.DeleteAddressAsync(id);

            return StatusCode(response.Status, response);
        }
    }
}
