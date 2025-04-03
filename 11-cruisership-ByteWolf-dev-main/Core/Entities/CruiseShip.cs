using Base.Core.Entities;

namespace Core.Entities;

public class CruiseShip : EntityObject
{
    public required string Name { get; set; }

    public uint YearOfConstruction { get; set; }

    public uint? Tonnage { get; set; }

    public decimal? Length { get; set; }

    public uint? Cabins { get; set; }

    public uint? Passengers { get; set; }

    public uint? Crew { get; set; }

    public string? Remark { get; set; }

    public int?             ShippingCompanyId { get; set; }
    public ShippingCompany? ShippingCompany   { get; set; }

    public IList<ShipName>? ShipNames { get; set; }
}