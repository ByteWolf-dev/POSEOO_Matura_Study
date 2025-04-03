using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public IShipNameRepository        ShipNameRepository        { get; }
    public ICruiseShipRepository      CruiseShipRepository      { get; }
    public IShippingCompanyRepository ShippingCompanyRepository { get; }

    public UnitOfWork(ApplicationDbContext context,
        IShipNameRepository                shipNameRepository,
        ICruiseShipRepository              cruiseShipRepository,
        IShippingCompanyRepository         shippingCompanyRepository
    ) : base(context)
    {
        ShipNameRepository        = shipNameRepository;
        CruiseShipRepository      = cruiseShipRepository;
        ShippingCompanyRepository = shippingCompanyRepository;
    }
}