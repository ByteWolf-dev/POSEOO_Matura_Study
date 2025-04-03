using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Base.Persistence;

public class ShipNameRepository : GenericRepository<ShipName>, IShipNameRepository
{
    public ShipNameRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}