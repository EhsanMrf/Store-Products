using System.Transactions;
using Common.OperationCrud;
using Microsoft.Extensions.DependencyInjection;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Infrastructure.Database;

namespace StoreProducts.Test;

public class RealDatabaseFixture : IDisposable
{
    private readonly TransactionScope _transactionScope;
    public ICrudManager<Core.Product.Entity.Product, int, DatabaseContext>? DbContext;
    public RealDatabaseFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddServices();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        DbContext = serviceProvider.GetService<ICrudManager<Core.Product.Entity.Product, int, DatabaseContext>>();
        _transactionScope = new TransactionScope();
    }
    public void Dispose()
    {
        _transactionScope.Dispose();
    }
}