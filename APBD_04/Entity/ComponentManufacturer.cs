namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("Abbreviation")]
    public string Abbreviation { get; set; } = null!;

    [Required]
    [MaxLength(300)]
    [Column("Fullname")]
    public string Fullname { get; set; } = null!;

    public DateTime FoundationDate { get; set; }
}