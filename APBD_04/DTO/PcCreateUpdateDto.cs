using System.ComponentModel.DataAnnotations;

namespace APBD_04.DTO;

public class PcCreateUpdateDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public double Weight { get; set; }

    [Required]
    public int Warranty { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int Stock { get; set; }
}
