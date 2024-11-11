using System.ComponentModel.DataAnnotations;

namespace ProtectedWebApi.Models;

public record SearchCriteria
{
    [Required]
    public string Name { get; set; } = string.Empty;

    // By setting this to an empty string, the model state validation will automatically interpret it as required
    // if we wanted this to be optional then we would need to set the type as a nullable string
    [Required]
    public string Cuisine { get; set; } = string.Empty;
}
