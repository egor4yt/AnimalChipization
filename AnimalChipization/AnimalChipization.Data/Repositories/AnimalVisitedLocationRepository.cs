using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;

namespace AnimalChipization.Data.Repositories;

public class AnimalVisitedLocationRepository : RepositoryBase<AnimalVisitedLocation>, IAnimalVisitedLocationRepository
{
    public AnimalVisitedLocationRepository(ApplicationDbContext context) : base(context)
    {
    }
}