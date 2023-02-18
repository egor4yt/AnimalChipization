namespace AnimalChipization.Services.Models.Animal;

public class CreateAnimalModel
{
    public List<long> AnimalTypes { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public int ChipperId { get; set; }
    public long ChippingLocationId { get; set; }
    public string Gender { get; set; }
}