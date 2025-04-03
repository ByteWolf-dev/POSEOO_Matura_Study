namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICruiseShipRepository      CruiseShipRepository      { get; }
    public IShipNameRepository        ShipNameRepository        { get; }
    public IShippingCompanyRepository ShippingCompanyRepository { get; }
}