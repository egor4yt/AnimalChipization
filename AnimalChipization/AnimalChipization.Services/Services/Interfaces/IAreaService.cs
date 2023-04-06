using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Area;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAreaService
{
    Task<Area?> GetByIdAsync(long areaId);
    Task CreateAsync(Area area);
    Task DeleteAsync(long areaId);
    Task<Area> UpdateAsync(UpdateAreaModel model);
}