using BonProf.Models;
using Microsoft.EntityFrameworkCore;
using SemPaiGo.Contexts;
using SemPaiGo.Models;
using SemPaiGo.Utilities;
using System.Security.Claims;

namespace SemPaiGo.Services;

/// <summary>
/// Service pour la gestion des produits
/// </summary>
public class ProductService(MainContext context)
{
    /// <summary>
    /// Récupère tous les produits
    /// </summary>
    /// <returns>Liste des produits</returns>
    public async Task<Response<List<ProductDetails>>> GetAllProductsAsync()
    {
        try
        {
            var products = await context.Products
                .AsNoTracking()
                .Include(p => p.Cursus)
                .Where(p => p.ArchivedAt == null)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductDetails(p))
                .ToListAsync();

            return new Response<List<ProductDetails>>
            {
                Status = 200,
                Message = "Produits récupérés avec succès",
                Data = products,
                Count = products.Count
            };
        }
        catch (Exception ex)
        {
            return new Response<List<ProductDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des produits: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Récupère un produit par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>Produit trouvé</returns>
    public async Task<Response<ProductDetails>> GetProductByIdAsync(Guid id)
    {
        try
        {
            var product = await context.Products
                .AsNoTracking()
                .Include(p => p.Cursus)
                .FirstOrDefaultAsync(p => p.Id == id && p.ArchivedAt == null);

            if (product == null)
            {
                return new Response<ProductDetails>
                {
                    Status = 404,
                    Message = "Produit non trouvé",
                    Data = null
                };
            }

            return new Response<ProductDetails>
            {
                Status = 200,
                Message = "Produit récupéré avec succès",
                Data = new ProductDetails(product)
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération du produit: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Récupère les produits par cursus
    /// </summary>
    /// <param name="cursusId">Identifiant du cursus</param>
    /// <returns>Liste des produits du cursus</returns>
    public async Task<Response<List<ProductDetails>>> GetProductsByCursusIdAsync(Guid cursusId)
    {
        try
        {
            var products = await context.Products
                .AsNoTracking()
                .Include(p => p.Cursus)
                .Where(p => p.CursusId == cursusId && p.ArchivedAt == null)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductDetails(p))
                .ToListAsync();

            return new Response<List<ProductDetails>>
            {
                Status = 200,
                Message = "Produits du cursus récupérés avec succès",
                Data = products,
                Count = products.Count
            };
        }
        catch (Exception ex)
        {
            return new Response<List<ProductDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des produits du cursus: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Récupère les produits par teacherId
    /// </summary>
    /// <param name="teacherId">Identifiant du cursus</param>
    /// <returns>Liste des produits du cursus</returns>
    public async Task<Response<List<ProductDetails>>> GetProductsByTeacherIdAsync(Guid teacherId)
    {
        try
        {
            var products = await context.Products
                .AsNoTracking()
                .Include(p => p.Cursus)
                .Where(p => p.Cursus.TeacherId == teacherId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductDetails(p))
                .ToListAsync();

            return new Response<List<ProductDetails>>
            {
                Status = 200,
                Message = "Produits du cursus récupérés avec succès",
                Data = products,
                Count = products.Count
            };
        }
        catch (Exception ex)
        {
            return new Response<List<ProductDetails>>
            {
                Status = 500,
                Message = $"Erreur lors de la récupération des produits du cursus: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Crée un nouveau produit
    /// </summary>
    /// <param name="productDto">Données du produit à créer</param>
    /// <returns>Produit créé</returns>
    public async Task<Response<ProductDetails>> CreateProductAsync(ProductCreate productDto, ClaimsPrincipal User)
    {
        try
        {
            var user = CheckUser.GetUserFromClaim(User, context);
            if(user is null)
            {
                return new Response<ProductDetails>
                {
                    Status = 401,
                    Message = "Utilisateur non authentifié",
                    Data = null
                };
            }

            var cursus = await context.Cursuses
                .FirstOrDefaultAsync(c => c.Id == productDto.CursusId && c.ArchivedAt == null && c.TeacherId == user.Id);

            if (cursus is null)
            {
                return new Response<ProductDetails>
                {
                    Status = 401,
                    Message = "Cours non existant",
                    Data = null
                };
            }

            var product = new Product(productDto);

            context.Products.Add(product);
            await context.SaveChangesAsync();

            // Recharger avec les relations pour la réponse
            var createdProduct = await context.Products
                .Include(p => p.Cursus)
                .FirstAsync(p => p.Id == product.Id);

            return new Response<ProductDetails>
            {
                Status = 201,
                Message = "Produit créé avec succès",
                Data = new ProductDetails(createdProduct)
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la création du produit: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Met à jour un produit existant
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <param name="productDto">Nouvelles données du produit</param>
    /// <returns>Produit mis à jour</returns>
    public async Task<Response<ProductDetails>> UpdateProductAsync(ProductUpdate productDto)
    {
        try
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == productDto.Id && p.ArchivedAt == null);

            if (product == null)
            {
                return new Response<ProductDetails>
                {
                    Status = 404,
                    Message = "Produit non trouvé",
                    Data = null
                };
            }

            productDto.UpdateProduct(product);

            await context.SaveChangesAsync();

            // Recharger avec les relations pour la réponse
            var updatedProduct = await context.Products
                .Include(p => p.Cursus)
                .FirstAsync(p => p.Id == productDto.Id);

            return new Response<ProductDetails>
            {
                Status = 200,
                Message = "Produit mis à jour avec succès",
                Data = new ProductDetails(updatedProduct)
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDetails>
            {
                Status = 500,
                Message = $"Erreur lors de la mise à jour du produit: {ex.Message}",
                Data = null
            };
        }
    }

    /// <summary>
    /// Archive un produit (suppression logique)
    /// </summary>
    /// <param name="id">Identifiant du produit</param>
    /// <returns>Résultat de l'opération</returns>
    public async Task<Response<object>> DeleteProductAsync(Guid id)
    {
        try
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.ArchivedAt == null);

            if (product == null)
            {
                return new Response<object>
                {
                    Status = 404,
                    Message = "Produit non trouvé",
                    Data = null
                };
            }

            product.ArchivedAt = DateTimeOffset.UtcNow;
            product.UpdatedAt = DateTimeOffset.UtcNow;

            await context.SaveChangesAsync();

            return new Response<object>
            {
                Status = 200,
                Message = "Produit supprimé avec succès",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new Response<object>
            {
                Status = 500,
                Message = $"Erreur lors de la suppression du produit: {ex.Message}",
                Data = null
            };
        }
    }
}
