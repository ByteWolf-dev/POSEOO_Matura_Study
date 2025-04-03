using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

using Core.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

public interface IShippingCompanyRepository : IGenericRepository<ShippingCompany>
{
    //TODO Add methods, e.g. GetOverviewAsync() returning IList<CompanyOverview>;
}