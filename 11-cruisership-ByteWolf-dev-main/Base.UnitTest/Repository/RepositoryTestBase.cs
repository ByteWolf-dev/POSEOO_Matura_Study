namespace Base.UnitTest.Repository;

using Microsoft.EntityFrameworkCore;

public class RepositoryTestBase<TDbContext> where TDbContext : DbContext
{
    protected RepositoryTestBase(RepositoryTestFixtureBase<TDbContext> testFixture)
    {
        TestFixture = testFixture;
    }

    public RepositoryTestFixtureBase<TDbContext> TestFixture { get; }
}