namespace AnimalChipization.Data.Entities;

public class Location : EntityBase
{
    public long Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}