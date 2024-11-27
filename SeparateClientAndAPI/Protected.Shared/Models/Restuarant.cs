using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Protected.Shared.Models;

public record Restuarant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [BsonElement("cuisineType")]
    [Required]
    public string CuisineType { get; set; } = string.Empty;

    [BsonElement("website")]
    [Required]
    [Url]
    public string Url { get; set; } = string.Empty;

    [BsonElement("phone")]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [BsonElement("address")]
    public Location Address { get; set; } = new Location();
}
