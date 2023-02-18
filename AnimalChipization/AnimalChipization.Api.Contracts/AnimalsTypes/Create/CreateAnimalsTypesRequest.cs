using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.AnimalsTypes.Create;

public class CreateAnimalsTypesRequest
{
    [Required]
    public string Type { get; set; }
}