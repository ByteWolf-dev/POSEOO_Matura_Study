using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ShippingCompanyRepository : GenericRepository<ShippingCompany>, IShippingCompanyRepository
{
    public ShippingCompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}