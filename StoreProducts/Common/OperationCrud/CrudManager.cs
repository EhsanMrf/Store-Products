using System.Linq.Expressions;
using AutoMapper;
using Common.Entity;
using Common.Response;
using Microsoft.EntityFrameworkCore;
using Common.Util;

namespace Common.OperationCrud;

public class CrudManager<T, TId, TDatabase> : ICrudManager<T, TId, TDatabase> where T : BaseEntity<TId> where TId : struct, IComparable where TDatabase : DbContext
{
    private readonly IMapper _mapper;
    private readonly TDatabase dbContext;

    public CrudManager(IMapper mapper, TDatabase dbContext)
    {
        _mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<bool> Insert(T entity)
    {
        Utils.NotNull(entity);
        entity.CreateDateTime=DateTime.Now;
        dbContext.Entry(entity).State = EntityState.Added;
        return await dbContext.SaveChangesAsync() > 0;
    }
    public async Task<ServiceResponse<T>> Insert<T>(T entity)
    {
        Utils.NotNullEntity(entity);
        dbContext.Entry(entity).State = EntityState.Added;
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<ServiceResponse<T>> UpdateByIdObject(TId id, object inputEntity)
    {
        var oldEntity = await GetById(id);
        Utils.NotNull(oldEntity);
        var entity = _mapper.Map(inputEntity, oldEntity.Data);
        entity.UpdateDateTime= DateTime.Now;
        dbContext.Entry(entity).State = EntityState.Modified;
        var save = await dbContext.SaveChangesAsync() > 0;
        Utils.StateOperation(save);
        return entity;
    }

    public async Task<bool> UpdateById(TId id, object inputEntity)
    {
        var oldEntity = await GetById(id);
        Utils.NotNull(oldEntity);
        var entity = _mapper.Map(inputEntity, oldEntity.Data);
        dbContext.Entry(entity).State = EntityState.Modified;
        var save = await dbContext.SaveChangesAsync() > 0;
        Utils.StateOperation(save);
        return save;
    }

    public async Task<ServiceResponse<bool>> DeleteById(TId id)
    {
        var entity = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        Utils.NotNull(entity);
        entity.IsDeleted = true;
        dbContext.Entry(entity).State = EntityState.Modified;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ServiceResponse<T>> GetById(TId id)
    {
        var data = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        Utils.NotNull(data);
        return data;
    }

    public async Task<bool> HasRecord(Expression<Func<T, bool>> predicate)
    {
        return await dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
    }

    public async Task<TResult?> SelectByPredicate<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> predicateSelect)
    {
        return await dbContext.Set<T>().Where(predicate).Select(predicateSelect).FirstOrDefaultAsync();
    }

    public DbSet<T> GetEntity()=> dbContext.Set<T>();

}

public interface ICrudManager<T, in TId, in TDatabase> where T : BaseEntity<TId> where TId : struct, IComparable where TDatabase : DbContext
{
    Task<bool> Insert(T entity);
    Task<ServiceResponse<T>> Insert<T>(T serviceEntity);
    Task<ServiceResponse<T>> UpdateByIdObject(TId id, object inputEntity);
    Task<bool> UpdateById(TId id, object inputEntity);
    Task<ServiceResponse<bool>> DeleteById(TId id);
    Task<ServiceResponse<T>> GetById(TId id);
    Task<bool> HasRecord(Expression<Func<T, bool>> predicate);
    Task<TResult?> SelectByPredicate<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> predicateSelect);
    DbSet<T> GetEntity();
}