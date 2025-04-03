using Base.Core.Entities;

namespace Core.Entities;

public class ShippingCompany : EntityObject
{
    public required string Name { get; set; }

    public string? City     { get; set; }
    public string? PLZ      { get; set; }
    public string? Street   { get; set; }
    public string? StreetNo { get; set; }

    public IList<CruiseShip>? CruiseShips { get; set; }
}