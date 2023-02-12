using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;

namespace AnimalChipization.Data.Repositories;

public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    // Can bee extended by any additional methods that do not present in RepositoryBase
}