using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalChipization.Data.Entities;

public class Account : EntityBase
{
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(128)")]
    public string FirstName { get; set; }

    [Required]
    [Column(TypeName = "varchar(128)")]
    public string LastName { get; set; }

    [Required]
    [Column(TypeName = "varchar(128)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(128)")]
    public string Role { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public string PasswordHash { get; set; }

    public List<Animal> Animals { get; set; }
}