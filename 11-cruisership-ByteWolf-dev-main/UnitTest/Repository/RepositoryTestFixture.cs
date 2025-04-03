using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Xunit;

namespace UnitTest.Repository;

using System.Collections.Generic;
using System.Linq;

using Base.Core.Contracts;
using Base.UnitTest.Repository;

public class RepositoryTestFixture : RepositoryTestFixtureBase<ApplicationDbContext>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        //drop and recreate the test Db every time. 

        await InitializeDatabase();
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }

    private async Task InitializeDatabase()
    {
        using (var dbContext = CreateDbContext())
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.MigrateAsync();

            await ImportDataAsync(dbContext);

            await dbContext.SaveChangesAsync();
        }
    }


    private async Task ImportDataAsync(ApplicationDbContext dbContext)
    {
        var shippingCompanies = (await new CsvImport<ShippingCompany>().ReadAsync("Repository/TestData/ShippingCompany.csv")).ToDictionary(s => s.Id);
        var cruiseShips       = (await new CsvImport<CruiseShip>().ReadAsync("Repository/TestData/CruiseShip.csv")).ToDictionary(l => l.Id);
        var shipNames         = (await new CsvImport<ShipName>().ReadAsync("Repository/TestData/ShipName.csv")).ToDictionary(w => w.Id);

        foreach (var shipName in shipNames.Values)
        {
            shipName.CruiseShip   = cruiseShips[shipName.CruiseShipId];
            shipName.CruiseShipId = 0;
        }

        foreach (var cruiseShip in cruiseShips.Values)
        {
            cruiseShip.ShippingCompany   = cruiseShip.ShippingCompanyId.HasValue ? shippingCompanies[cruiseShip.ShippingCompanyId.Value] : null;
            cruiseShip.ShippingCompanyId = null;
        }

        void SetIdNull<T>(IList<T> list) where T : IEntityObject
        {
            foreach (var item in list)
            {
                item.Id = 0;
            }
        }

        SetIdNull(cruiseShips.Values.ToList());
        SetIdNull(shippingCompanies.Values.ToList());
        SetIdNull(shipNames.Values.ToList());

        await dbContext.Set<ShippingCompany>().AddRangeAsync(shippingCompanies.Values.ToList());
        await dbContext.Set<ShipName>().AddRangeAsync(shipNames.Values.ToList());
        await dbContext.Set<CruiseShip>().AddRangeAsync(cruiseShips.Values.ToList());
    }

    public override ApplicationDbContext CreateDbContext()
    {
        var connectString = @"Data Source = (LocalDB)\MSSQLLocalDB;Initial Catalog = Cruiser_Test;Integrated Security = True";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(connectString, x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}