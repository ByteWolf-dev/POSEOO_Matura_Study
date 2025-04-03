namespace UnitTest.ViewModels;

using System.Linq.Expressions;
using System.Linq;
using System;
using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using FluentAssertions;

using NSubstitute;

using Wpf.ViewModels;

using Xunit;

public class CompanyShipsViewModelTests
{
    [Fact]
    public void EditButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new CompanyShipsViewModel(uow);

        //Act

        mv.SelectedShip = null;
        mv.EditShipCommand.CanExecute(null).Should().BeFalse();
        mv.UpdateShipCommand.CanExecute(null).Should().BeFalse();

        mv.SelectedShip = new CruiseShip() { Name = "Dummy" };
        mv.EditShipCommand.CanExecute(null).Should().BeTrue();
        mv.UpdateShipCommand.CanExecute(null).Should().BeFalse();

        mv.EditShipCommand.Execute(null);

        mv.EditShipCommand.CanExecute(null).Should().BeFalse();
        mv.UpdateShipCommand.CanExecute(null).Should().BeTrue();

        mv.SelectedShip = new CruiseShip() { Name = "Dummy" };

        mv.EditShipCommand.CanExecute(null).Should().BeTrue();
        mv.UpdateShipCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow                  = Substitute.For<IUnitOfWork>();
        var cruiseShipRepository = Substitute.For<ICruiseShipRepository>();

        uow.CruiseShipRepository.Returns(cruiseShipRepository);

        var ships = new CsvImport<CruiseShip>().Read(
            new[]
            {
                "Id;Name;YearOfConstruction;Passengers;Crew",
                "1;NameXX;1999;;",
                "1;NamePX;1999;10;",
                "1;NameXC;1999;;100",
                "1;NamePC;1999;20;200",
            });

        //TODO: Uncomment if implemented
        throw new NotImplementedException();
//        uow.CruiseShipRepository
//            .GetByCompanyIdNoTrackingAsync(1)
//            .Returns(ships);

        var mv = new CompanyShipsViewModel(uow);
        mv.CompanyId = 1;

        //Act

        await mv.InitializeDataAsync();

        //Assert

        mv.Ships.Should()
            .NotBeEmpty()
            .And.HaveCount(ships.Count)
            .And.Contain(ships);

        // load again
        await mv.InitializeDataAsync();
        mv.Ships.Should()
            .NotBeEmpty()
            .And.HaveCount(ships.Count)
            .And.Contain(ships);
    }


    [Fact]
    public void SelectMoveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new CompanyShipsViewModel(uow);

        mv.SelectedShip = new CruiseShip()
        {
            Id         = 14,
            Name       = "Dummy",
            Passengers = 100,
            Crew       = 200
        };

        //Act
        //Assert

        mv.Passengers.Should().Be(100);
        mv.Crew.Should().Be(200);

        // revert edit
        mv.SelectedShip = new CruiseShip()
        {
            Id         = 15,
            Name       = "Dummy",
            Passengers = 200,
            Crew       = 400
        };

        mv.Passengers.Should().Be(200);
        mv.Crew.Should().Be(400);

        mv.EditShipCommand.Execute(null);
        mv.IsEditMode.Should().BeTrue();

        mv.SelectedShip = null;

        mv.IsEditMode.Should().BeFalse();

        mv.Passengers.Should().BeNull();
        mv.Crew.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTest()
    {
        //Arrange

        var uow       = Substitute.For<IUnitOfWork>();

        var mv = new CompanyShipsViewModel(uow);
        var testMove = new CruiseShip()
        {
            Id         = 14,
            Name       = "Dummy",
            Passengers = 100,
            Crew       = 200
        }; ;

        mv.SelectedShip = testMove;

        uow.CruiseShipRepository
            .GetByIdAsync(Arg.Any<int>())
            .Returns(mv.SelectedShip);

        //Act/Assert

        // with modification

        mv.EditShipCommand.Execute(null);
        mv.Passengers = mv.Passengers + 1;
        mv.UpdateShipCommand.Execute(null);

        mv.IsEditMode.Should().BeFalse();

        await uow.Received().SaveChangesAsync();
    }
}