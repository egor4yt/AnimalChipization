namespace AnimalChipization.Api.Contracts.Animals.AttachAnimalType;

public class AttachAnimalTypeAnimalsResponse
{
    public long Id { get; set; }
    public virtual List<long> AnimalTypes { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public string Gender { get; set; }
    public string LifeStatus { get; set; }
    public string ChippingDateTime { get; set; }
    public int ChipperId { get; set; }
    public long ChippingLocationId { get; set; }
    public List<long> VisitedLocations { get; set; }
    public string? DeathDateTime { get; set; }
}