using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalChipization.Data.Entities;

public abstract class EntityBase
{
    public DateTime CreatedAt { get; set; }
}