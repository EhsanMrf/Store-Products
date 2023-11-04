using FluentAssertions;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Core.User.Command;

namespace StoreProducts.Test;

public class ProductUnitTestCommandQuery
{
    [Fact]
    public void ProductShouldNotNull()
    {
        var course = () => { var product = new CreateProductCommand(); };
        course.Should().NotBeNull();
    }

    [Fact]
    public void CreateProductShouldValidate()
    {
        var course = new CreateProductCommand
        {
            Name = "NameProduct",
            ManufacturePhone = "09127542323",
            ManufactureEmail = "ehsanmrf@email.com",
            IsAvailable = true,
            ProduceDate = DateTime.Now
        };
        course.Name.Should().NotBeNullOrEmpty().And.MatchRegex(".{3,}");
        course.ManufacturePhone.Should().HaveLength(11).And.MatchRegex("^09\\d{9}$");
        course.ManufactureEmail.Should().MatchRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$");
        course.IsAvailable.Should().BeTrue();
        course.ProduceDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void DeleteProductShouldValidate()
    {
        var course = new DeleteProductCommand
        {
            Id = 1
        };
        course.Id.Should().Be(1);
    }

    [Fact]
    public void UpdateProductShouldValidate()
    {
        var course = new UpdateProductCommand
        {
            Id = 1,
            ManufactureEmail = "ehsanmrf@email.com",
            ManufacturePhone = "09127542369",
            Name = "NameProduct"
        };
        course.Name.Should().NotBeNullOrEmpty().And.MatchRegex(".{3,}");
        course.ManufacturePhone.Should().HaveLength(11).And.MatchRegex("^09\\d{9}$");
        course.ManufactureEmail.Should().MatchRegex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$");
        course.Id.Should().Be(1);
    }
}