namespace Core.Entities;

using Base.Core.Entities;

public class ShipName : EntityObject
{
    public required string Name { get; set; }

    public int         CruiseShipId { get; set; }
    public CruiseShip? CruiseShip   { get; set; }
}