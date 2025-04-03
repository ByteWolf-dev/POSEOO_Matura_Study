using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

using Core.DataTransferObjects;

using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

public class CruiseShipRepository : GenericRepository<CruiseShip>, ICruiseShipRepository
{
    public CruiseShipRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}