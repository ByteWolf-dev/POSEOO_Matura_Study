namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ShipNameMapping
{
    public static void Map(this EntityTypeBuilder<ShipName> entity)
    {
        entity.ToTable("ShipName");
        entity.HasKey(d => d.Id);
        entity.HasIndex(d => new { d.Id, d.Name }).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);

        entity.HasOne(s => s.CruiseShip).WithMany(c => c.ShipNames).HasForeignKey(t => t.CruiseShipId);
    }
}