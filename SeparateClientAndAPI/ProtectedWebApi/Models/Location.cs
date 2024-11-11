using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProtectedWebApi.Models;

public record Location
{
    [BsonElement("street")]
    [Required]
    public string Street { get; set; } = string.Empty;

    [BsonElement("city")]
    [Required]
    public string City { get; set; } = string.Empty;

    [BsonElement("state")]
    [Required]
    [StringLength(2)]
    public string State { get; set; } = string.Empty;

    [BsonElement("country")]
    [Required]
    public string Country { get; set; } = "United States";

    [BsonElement("zipCode")]
    [Required]
    [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Zip Code must be in one of the following formats: xxxxx, xxxxx xxxx, xxxxx-xxxx")]
    public string ZipCode { get; set; } = string.Empty;
}
