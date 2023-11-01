using Common.Entity;
using Common.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
namespace Common.OperationCrud;

public class CrudManager<T, TId> : ICrudManager<T, TId> where T : BaseEntity<TId> where TId : struct, IComparable
{

    public async Task<bool> Insert(DbContext dbContext, T entity)
    {
        if (entity is null)
            return false;
        dbContext.Entry(entity).State = EntityState.Added;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ServiceResponse<T>> Insert(DbContext dbContext, ServiceResponse<T> entity)
    {
        if (entity.Data is null)
            return entity;
        dbContext.Entry(entity.Data).State = EntityState.Added;
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<ServiceResponse<T>> GetById(DbContext dbContext, ServiceResponse<T> entity, TId id)
    {
        var data = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        entity.Data = data;
        return entity.Data is not null ? entity : new ServiceResponse<T> { Data = null, Message = "No Data" };
    }
}

public interface ICrudManager<T, TId> where T : BaseEntity<TId> where TId : struct, IComparable
{
    Task<bool> Insert(DbContext dbContext, T entity);
    Task<ServiceResponse<T>> Insert(DbContext dbContext, ServiceResponse<T> entity);
    Task<ServiceResponse<T>> GetById(DbContext dbContext, ServiceResponse<T> entity, TId id);
}