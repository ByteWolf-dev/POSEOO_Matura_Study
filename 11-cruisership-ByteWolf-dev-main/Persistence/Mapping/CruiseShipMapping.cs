namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CruiseShipMapping
{
    public static void Map(this EntityTypeBuilder<CruiseShip> entity)
    {
        entity.ToTable("CruiseShip");
        entity.HasKey(t => t.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).IsUnicode(false).HasMaxLength(256);

        entity.Property(d => d.Length).AsDecimal(6, 2);


        entity.HasOne(c => c.ShippingCompany).WithMany(g => g.CruiseShips).HasForeignKey(c => c.ShippingCompanyId);
    }
}