using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using BonProf.Models;
using SemPaiGo.Models.Interfaces;
using SemPaiGo.Utilities;

namespace SemPaiGo.Models;

public class Address : BaseModel
{
    [Required]
    public required string Street { get; set; }

    [Required]
    public required string City { get; set; }



    [Required]
    public required string Country { get; set; }

    [Required]
    public required string ZipCode { get; set; }
    public string? AdditionalInfo { get; set; }
    public float? Longitude { get; set; }
    public float? Latitude { get; set; }
    public Guid ProfileId { get; set; }
    public Profile? Profile { get; set; }
    public Guid TypeId { get; set; } = HardCode.TYPE_ADDRESS_HOME;
    public TypeAddress? Type { get; set; }
    public Address()
    {        
    }

    [SetsRequiredMembers]
    public Address(AddressCreate addressDto)
    {
        Id = Guid.NewGuid();
        Street = addressDto.Street;
        City = addressDto.City;
        Country = addressDto.Country;
        ZipCode = addressDto.ZipCode;
        AdditionalInfo = addressDto.AdditionalInfo;
        Longitude = addressDto.Longitude;
        Latitude = addressDto.Latitude;
        ProfileId = addressDto.UserId;
        TypeId =  HardCode.TYPE_ADDRESS_HOME;//addressDto.TypeId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
    public void UpdateAddress(AddressUpdate addressDto)
    {
        Street = addressDto.Street;
        City = addressDto.City;
        Country = addressDto.Country;
        ZipCode = addressDto.ZipCode;
        AdditionalInfo = addressDto.AdditionalInfo;
        Longitude = addressDto.Longitude;
        Latitude = addressDto.Latitude;
        UpdatedAt = DateTimeOffset.UtcNow;
        TypeId = addressDto.TypeId;
    }
}
