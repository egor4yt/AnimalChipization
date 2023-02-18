using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalChipization.Data.Entities;

public class AnimalType : EntityBase
{
    public long Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(128)")]
    public string Type { get; set; }
}