using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAreaService
{
    Task<Area?> GetByIdAsync(long areaId);
    Task CreateAsync(Area area);
    Task DeleteAsync(long areaId);
}