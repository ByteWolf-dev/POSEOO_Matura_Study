namespace Base.UnitTest.Repository;

using Microsoft.EntityFrameworkCore;

public abstract class RepositoryTestFixtureBase<TDbContext> where TDbContext : DbContext
{
    protected RepositoryTestFixtureBase()
    {
    }

    public abstract TDbContext CreateDbContext();
}