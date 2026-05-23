namespace APBD_04.DTO;

public class PcWithComponentsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<PcComponentDto> Components { get; set; } = new();
}

public class PcComponentDto
{
    public int Amount { get; set; }
    public ComponentDetailDto Component { get; set; } = null!;
}

public class ComponentDetailDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ComponentManufacturerDto Manufacturer { get; set; } = null!;
    public ComponentTypeDto Type { get; set; } = null!;
}

public class ComponentManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime FoundationDate { get; set; }
}

public class ComponentTypeDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
}
