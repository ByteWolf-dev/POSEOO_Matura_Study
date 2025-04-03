using Base.Core.Contracts;

using Core.Entities;

namespace Core.Contracts;

using Core.DataTransferObjects;

using System.Threading.Tasks;

public interface ICruiseShipRepository : IGenericRepository<CruiseShip>
{
    //TOOD Add methods, e.g. GetByCompanyIdNoTrackingAsync(companyId);
}