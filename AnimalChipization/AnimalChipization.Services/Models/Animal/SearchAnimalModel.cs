namespace AnimalChipization.Services.Models.Animal;

public class SearchAnimalModel
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? ChipperId { get; set; }
    public long? ChippingLocationId { get; set; }
    public string? LifeStatus { get; set; }
    public string? Gender { get; set; }
    public int From { get; set; }
    public int Size { get; set; }
}