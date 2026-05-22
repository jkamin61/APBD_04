namespace APBD_04.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PCComponents")]
public class PcComponent
{
    [Column("PCId")]
    public int PcId { get; set; }

    [Column(TypeName = "char(10)")]
    [StringLength(10)]
    public string ComponentCode { get; set; } = null!;

    [Required]
    public int Amount { get; set; }

    [ForeignKey(nameof(PcId))]
    public PC Pc { get; set; } = null!;

    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; } = null!;
}