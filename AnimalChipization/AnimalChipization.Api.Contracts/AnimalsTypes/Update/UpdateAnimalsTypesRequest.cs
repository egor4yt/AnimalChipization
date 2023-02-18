using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.AnimalsTypes.Update;

public class UpdateAnimalsTypesRequest
{
    [Required]
    public string Type { get; set; }
}