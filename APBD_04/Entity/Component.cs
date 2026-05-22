namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Components")]
public class Component
{
    [Key]
    [Column(TypeName = "char(10)")]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Description { get; set; } = null!;

    public int ComponentManufacturerId { get; set; }

    public int ComponentTypeId { get; set; }

    [ForeignKey(nameof(ComponentManufacturerId))]
    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

    [ForeignKey(nameof(ComponentTypeId))]
    public ComponentType ComponentType { get; set; } = null!;

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}