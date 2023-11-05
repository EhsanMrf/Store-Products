using Common.OperationCrud;
using Common.Response.PageListExtension;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Infrastructure.Database;

namespace StoreProducts.Test.Product;

public class CourseCrudProductTest : IClassFixture<RealDatabaseFixture>
{
    public readonly ICrudManager<Core.Product.Entity.Product, int, DatabaseContext>? DbContext;

    public CourseCrudProductTest(RealDatabaseFixture databaseFixture)
    {
        DbContext = databaseFixture.DbContext;
    }

    [Fact]
    public void CrudManagerShouldBeNouNull()
    {
        var course = DbContext;
        course.Should().NotBeNull();
    }

    [Fact]
    public void Should_ReturnAllProducts()
    {
        var course = DbContext?.GetEntity().AsNoTracking().ToList();
        course.Should().HaveCountGreaterOrEqualTo(1);
    }

    [Fact]
    public void Should_CreateProduct_ReturnTrue()
    {
        var courser = DbContext?.Insert(new Core.Product.Entity.Product
        {
            CreateByUserId = 1,
            IsAvailable = true,
            Name = "Name",
            ManufactureEmail = "ehsanMrf@gmail.com",
            ManufacturePhone = "09127542365",
            ProduceDate = DateTime.Now,
        });
        courser!.Result.Should().BeTrue();
    }

    [Fact]
    public void Should_UpdateProduct_ReturnTrue()
    {
        var courser = DbContext!.UpdateById(20, new UpdateProductCommand
        {
            ManufactureEmail = "ehsanMaarefvand@gmail.com",
            Name = "UpdateName",
            ManufacturePhone = "09353855564",
            Id = 20
        }).Result;

        courser.Should().BeTrue();
    }

    [Fact]
    public void Should_DeleteProduct_ReturnTrue()
    {
        var courser = DbContext!.DeleteById(20).Result;
        courser.Data.Should().BeTrue();
    }


}