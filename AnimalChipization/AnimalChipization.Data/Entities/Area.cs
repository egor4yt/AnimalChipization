using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Data.Entities;

public class Area : EntityBase
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Polygon AreaPoints { get; set; }
}