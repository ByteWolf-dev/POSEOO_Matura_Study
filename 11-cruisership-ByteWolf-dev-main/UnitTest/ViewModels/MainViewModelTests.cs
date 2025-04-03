namespace UnitTest.ViewModels;

using System;
using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.DataTransferObjects;

using FluentAssertions;

using NSubstitute;

using Wpf.ViewModels;

using Xunit;

public class MainViewModelTests
{
    [Fact]
    public void DetailButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new MainWindowViewModel(uow);

        //Act/Assert

        mv.SelectedCompany = null;
        mv.ShowDetailCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow            = Substitute.For<IUnitOfWork>();
        var gameRepository = Substitute.For<IShippingCompanyRepository>();

        uow.ShippingCompanyRepository.Returns(gameRepository);

        var companies = new CompanyOverview[]
        {
            new (314, "Name 1", 10),
            new (315, "Name 2", 11),
            new (316, "Name 3", 12),
            new (317, "Name 4", 13),
        };

        //TODO: Uncomment if implemented
        throw new NotImplementedException();
//        uow.ShippingCompanyRepository.GetOverviewAsync()
//            .Returns(companies);

        var mv = new MainWindowViewModel(uow);

        //Act

        await mv.InitializeDataAsync();

        //Assert


        mv.Companies.Should()
            .NotBeEmpty()
            .And.HaveCount(companies.Length)
            .And.Contain(companies);

        // load again
        await mv.InitializeDataAsync();
        mv.Companies.Should()
            .NotBeEmpty()
            .And.HaveCount(companies.Length)
            .And.Contain(companies);
    }
}