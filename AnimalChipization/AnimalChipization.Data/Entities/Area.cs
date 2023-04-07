using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Data.Entities;

public class Area : EntityBase
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string AreaPoints { get; set; }
}