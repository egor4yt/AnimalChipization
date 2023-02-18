using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Data.Entities;

public class Location : EntityBase
{
    public long Id { get; set; }

    [Required]
    public double Longitude { get; set; }

    [Required]
    public double Latitude { get; set; }

    public List<Animal> Animals { get; set; }
}