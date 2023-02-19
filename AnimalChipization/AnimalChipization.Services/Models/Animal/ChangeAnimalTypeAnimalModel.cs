namespace AnimalChipization.Services.Models.Animal;

public class ChangeAnimalTypeAnimalModel
{
    public long AnimalId { get; set; }
    public long OldTypeId { get; set; }
    public long NewTypeId { get; set; }
}