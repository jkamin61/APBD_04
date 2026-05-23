namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComponentManufacturers")]
public class ComponentManufacturer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Abbreviation { get; set; } = null!;

    [Required]
    [MaxLength(300)]
    public string FullName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
