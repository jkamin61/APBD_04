namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PCs")]
public class Pc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "float(5)")]
    public double Weight { get; set; }

    [Required]
    public int Warranty { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int Stock { get; set; }

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}