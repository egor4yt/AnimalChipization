using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.AnimalsTypes.Post;

public class PostAnimalsTypesRequest
{
    [Required]
    public string Type { get; set; }
}