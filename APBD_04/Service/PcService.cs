using APBD_04.Data;
using APBD_04.DTO;
using APBD_04.Entity;
using Microsoft.EntityFrameworkCore;

namespace APBD_04.Service;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcDto>> GetAllAsync()
    {
        return await _context.Pcs
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new PcDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<PcWithComponentsDto?> GetWithComponentsAsync(int id)
    {
        var pc = await _context.Pcs
            .AsNoTracking()
            .Include(p => p.PcComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.ComponentManufacturer)
            .Include(p => p.PcComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.ComponentType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc is null)
        {
            return null;
        }

        return MapToPcWithComponents(pc);
    }

    public async Task<PcDto?> CreateAsync(PcCreateUpdateDto dto)
    {
        var nextId = await _context.Pcs.AnyAsync()
            ? await _context.Pcs.MaxAsync(p => p.Id) + 1
            : 1;

        var pc = new Pc
        {
            Id = nextId,
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return MapToPcDto(pc);
    }

    public async Task<PcDto?> UpdateAsync(int id, PcCreateUpdateDto dto)
    {
        var pc = await _context.Pcs.FindAsync(id);
        if (pc is null)
        {
            return null;
        }

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return MapToPcDto(pc);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);
        if (pc is null)
        {
            return false;
        }

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }

    private static PcDto MapToPcDto(Pc pc) => new()
    {
        Id = pc.Id,
        Name = pc.Name,
        Weight = pc.Weight,
        Warranty = pc.Warranty,
        CreatedAt = pc.CreatedAt,
        Stock = pc.Stock
    };

    private static PcWithComponentsDto MapToPcWithComponents(Pc pc) => new()
    {
        Id = pc.Id,
        Name = pc.Name,
        Weight = pc.Weight,
        Warranty = pc.Warranty,
        CreatedAt = pc.CreatedAt,
        Stock = pc.Stock,
        Components = pc.PcComponents.Select(pc => new PcComponentDto
        {
            Amount = pc.Amount,
            Component = new ComponentDetailDto
            {
                Code = pc.Component.Code.Trim(),
                Name = pc.Component.Name,
                Description = pc.Component.Description,
                Manufacturer = new ComponentManufacturerDto
                {
                    Id = pc.Component.ComponentManufacturer.Id,
                    Abbreviation = pc.Component.ComponentManufacturer.Abbreviation,
                    FullName = pc.Component.ComponentManufacturer.FullName,
                    FoundationDate = pc.Component.ComponentManufacturer.FoundationDate
                },
                Type = new ComponentTypeDto
                {
                    Id = pc.Component.ComponentType.Id,
                    Abbreviation = pc.Component.ComponentType.Abbreviation,
                    Name = pc.Component.ComponentType.Name
                }
            }
        }).ToList()
    };
}
