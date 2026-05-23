namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ComponentTypes")]
public class ComponentType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Abbreviation { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
