namespace UnitTest.WebApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;

using FluentAssertions;

using global::WebApi.Controllers;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using UnitTest.WebApi.Fixtures;

using Xunit;

public class ShippingCompanyControllerTests : IntegrationTest
{
    public ShippingCompanyControllerTests(ApiWebApplicationFactory fixture)
        : base(fixture)
    {
    }

    protected IUnitOfWork GetUnitOfWork(bool empty)
    {
        var uow              = Substitute.For<IUnitOfWork>();
        var officeRepository = Substitute.For<IShippingCompanyRepository>();

        var offices = new CsvImport<ShippingCompany>().Read(
            """
                Id;No;Name;Address
                1;16906;GASCHURN;VBG
                2;726;RAABS/THAYA;NOE
                3;901;RETZ;NOE
                4;2910;SCHAERDING;OOE
                """
                .Replace("\r", "").Split('\n'));

        if (!empty)
        {
            officeRepository.GetNoTrackingAsync(
                    Arg.Any<Expression<Func<ShippingCompany, bool>>>(),
                    Arg.Any<Func<IQueryable<ShippingCompany>, IOrderedQueryable<ShippingCompany>>>(),
                    Arg.Any<string[]>())
                .Returns(offices);

            officeRepository.GetByIdAsync(1)
                .Returns(offices.Single(s => s.Id == 1));

            uow.ShippingCompanyRepository.Returns(officeRepository);
        }

        return uow;
    }

    #region get

    protected HttpClient GetClient(bool empty)
    {
        return _factory
            .WithWebHostBuilder(builder => { builder.ConfigureServices(services => { services.AddSingleton<IUnitOfWork>(GetUnitOfWork(empty));});})
            .CreateClient();
    }

    [Fact]
    public async Task GetAllTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany");
        response.EnsureSuccessStatusCode();

        var dtos = await response.Content.ReadAsAsync<IList<ShippingCompanyController.ShippingCompanyDto>>();

        dtos.Should().HaveCount(4);

        dtos.Single(s => s.Id == 1).Should().BeEquivalentTo(
            new
            {
                Id      = 1,
                No      = "16906",
                Name    = "GASCHURN",
                Address = "VBG",
            });
    }

    [Fact]
    public async Task GetAllEmptyTest()
    {
        var client = GetClient(true);

        var response = await client.GetAsync("/ShippingCompany");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany/1");
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadAsAsync<ShippingCompanyController.ShippingCompanyDto>();

        dto.Should().BeEquivalentTo(
            new
            {
                Id      = 1,
                No      = "16906",
                Name    = "GASCHURN",
                Address = "VBG",
            });
    }

    [Fact]
    public async Task GetFailTest()
    {
        var client = GetClient(false);

        var response = await client.GetAsync("/ShippingCompany/232");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion
}