namespace Persistence.Mapping;

using Base.Persistence.Mappings;

using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ShippingCompanyMapping
{
    public static void Map(this EntityTypeBuilder<ShippingCompany> entity)
    {
        entity.ToTable("ShippingCompany");
        entity.HasKey(l => l.Id);

        entity.HasIndex(d => d.Name).IsUnique();
        entity.Property(d => d.Name).AsRequiredText(256);


        entity.Property(d => d.City!).AsText(128);
        entity.Property(d => d.PLZ!).AsText(16);
        entity.Property(d => d.Street!).AsText(128);
        entity.Property(d => d.StreetNo!).AsText(16);
    }
}