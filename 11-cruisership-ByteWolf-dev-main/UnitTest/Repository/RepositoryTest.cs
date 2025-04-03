using Persistence;

using Xunit;

namespace UnitTest.Repository;

using Base.UnitTest.Repository;

using Core.Contracts;

[CollectionDefinition(nameof(MyRepositoryTests))]
public class MyRepositoryTests : ICollectionFixture<RepositoryTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection(nameof(MyRepositoryTests))]
public abstract class RepositoryTest : RepositoryTestBase<ApplicationDbContext>
{
    protected RepositoryTest(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    protected IUnitOfWork GetUnitOfWork()
    {
        var context                   = TestFixture.CreateDbContext();
        var cruiseShipRepository      = new CruiseShipRepository(context);
        var shippingCompanyRepository = new ShippingCompanyRepository(context);
        var shipNameRepository        = new ShipNameRepository(context);
        var uow                       = new UnitOfWork(context, shipNameRepository, cruiseShipRepository, shippingCompanyRepository);
        return uow;
    }
}