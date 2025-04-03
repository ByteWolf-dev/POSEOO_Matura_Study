using Base.Core;
using Base.Tools;

using Core.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence;

ConfigureDependencyInjector();
await RecreateDatabaseAsync();
await Import();

void ConfigureDependencyInjector()
{
    var configuration = ConfigurationHelper.GetConfiguration();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    AppService.ServiceCollection = new ServiceCollection();
    AppService.ServiceCollection
        .AddSingleton(configuration)
        .AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString))
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddTransient<ICruiseShipRepository, CruiseShipRepository>()
        .AddTransient<IShippingCompanyRepository, ShippingCompanyRepository>()
        .AddTransient<IShipNameRepository, ShipNameRepository>()
        .AddTransient<IImportService, ImportService>()
        ;

    AppService.BuildServiceProvider();
}

async Task RecreateDatabaseAsync()
{
    Console.WriteLine("=====================");
    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        Console.WriteLine("Deleting database ...");
        await uow.DeleteDatabaseAsync();

//        Console.WriteLine("Creating and migrating database ...");
//        await uow.CreateDatabaseAsync();

        Console.WriteLine("Recreating and migrating database ...");
        await uow.MigrateDatabaseAsync();
    }
}

async Task Import()
{
    Console.WriteLine("=====================");
    Console.WriteLine("Import");

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
        var uow           = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await importService.ImportDbAsync();

        int countShipNames = await uow.ShipNameRepository.CountAsync();
        int countShips = await uow.CruiseShipRepository.CountAsync();
        int cuntCompanies   = await uow.ShippingCompanyRepository.CountAsync();
        Console.WriteLine($" {countShipNames} ship-names stored in DB");
        Console.WriteLine($" {countShips} ships stored in DB");
        Console.WriteLine($" {cuntCompanies} companies stored in DB");
    }

    Console.WriteLine($"Import done");
}