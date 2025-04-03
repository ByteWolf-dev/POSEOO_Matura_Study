/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 1xHIF
 *--------------------------------------------------------------
 *              Musterlösung
 *--------------------------------------------------------------
 * Description: Cruiser UnitTest
 *--------------------------------------------------------------
 */

using System;
using System.Linq;
using System.Threading.Tasks;

using Core.Contracts;
using Core.Entities;

using FluentAssertions;

using Persistence;

using Xunit;

namespace UnitTest.Repository;

using FluentAssertions.Extensions;

[Collection(nameof(MyRepositoryTests))]
public sealed class CruiseShipRepositoryTests : RepositoryTest
{
    #region crt and overrides

    public CruiseShipRepositoryTests(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    #endregion

    [Fact]
    public async Task ReadAllTest()
    {
        var uow = GetUnitOfWork();

        var all = await uow.CruiseShipRepository.GetNoTrackingAsync((l) => l.Name == "Achille Lauro", null, nameof(CruiseShip.ShipNames));

        all.Should().HaveCount(1);

        var achilleLauro = all.Single();

        // Check Mapping!
        // Achille Lauro;1947;23.112;192,00;;900;400;;;1985 Entführung im Mittelmeer, ex Willem Ruys,1994 ausgebrannt und gesunken

        achilleLauro.Should().BeEquivalentTo(
            new
            {
                Name               = "Achille Lauro",
                YearOfConstruction = 1947,
                Tonnage            = 23112,
                Length             = 192.00m,
                Cabins             = (uint?)null,
                Passengers         = 900,
                Crew               = 400,
                Remark             = "1985 Entführung im Mittelmeer,1994 ausgebrannt und gesunken",
                ShipNames = new[]
                {
                    new
                    {
                        Name = "Willem Ruys",
                    },
                }
            });
    }
}