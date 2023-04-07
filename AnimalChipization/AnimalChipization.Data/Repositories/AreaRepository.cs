using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AreaRepository : RepositoryBase<Area>, IAreaRepository
{
    public AreaRepository(ApplicationDbContext context) : base(context)
    {
    }

}