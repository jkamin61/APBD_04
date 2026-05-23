using APBD_04.DTO;

namespace APBD_04.Service;

public interface IPcService
{
    Task<IEnumerable<PcDto>> GetAllAsync();
    Task<PcWithComponentsDto?> GetWithComponentsAsync(int id);
    Task<PcDto?> CreateAsync(PcCreateUpdateDto dto);
    Task<PcDto?> UpdateAsync(int id, PcCreateUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
