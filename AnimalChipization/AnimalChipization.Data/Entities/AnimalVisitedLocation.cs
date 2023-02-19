using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Data.Entities;

public class AnimalVisitedLocation : EntityBase
{
    public long Id { get; set; }

    [Required]
    public long AnimalId { get; set; }

    public virtual Animal Animal { get; set; }

    [Required]
    public long LocationId { get; set; }

    public virtual Location Location { get; set; }
}