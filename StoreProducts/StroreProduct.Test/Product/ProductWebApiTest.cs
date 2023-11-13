using Common.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RESTFulSense.Clients;
using StoreProducts.Core.Product.Query.Result;
using System.Net.Http;

namespace StoreProducts.Test.Product
{
    public class ProductWebApiTest
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductWebApiTest()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();

            // ایجاد نمونه IHttpClientFactory
            var serviceProvider = services.BuildServiceProvider();
            _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
        //[Fact]
        //public void Should_GetAllProducts()
        //{
        //    var httpClient = _httpClientFactory.CreateClient();

        //    var actual = httpClient.GetStringAsync("https://localhost:7037/api/Product/GetAll?Page=1&PageSize=3").Result;


        //    var serviceResponse = actual;
        //    actual.Should().NotBeNull();
        //}
    }
}