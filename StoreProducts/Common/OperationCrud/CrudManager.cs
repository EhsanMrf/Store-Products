using AutoMapper;
using Common.Entity;
using Common.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
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

    public async Task<bool> Insert( T entity)
    {
        Utils.NotNull(entity);
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

    public async Task<ServiceResponse<T>> UpdateById(ServiceResponse<T> serviceEntity, TId id, T inputEntity)
    {
        var oldEntity = await GetById(serviceEntity, id);
        Utils.NotNull(oldEntity);
        var entity = _mapper.Map(inputEntity, oldEntity.Data);
        dbContext.Entry(entity).State = EntityState.Modified;
        var save = await dbContext.SaveChangesAsync()>0;
        Utils.StateOperation(save);
        return serviceEntity.Data;
    }

    public async Task<ServiceResponse<bool>> DeleteById(TId id)
    {
        var entity = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        Utils.NotNull(entity);
        entity.IsDeleted=true;
        dbContext.Entry(entity).State = EntityState.Modified;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ServiceResponse<T>> GetById( ServiceResponse<T> serviceEntity, TId id)
    {
        var data = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        Utils.NotNull(data);
        serviceEntity.Data = data;
        return serviceEntity.Data is not null ? serviceEntity : new ServiceResponse<T> { Data = null, Message = "No Data" };
    }
}

public interface ICrudManager<T, in TId, in TDatabase> where T : BaseEntity<TId> where TId : struct , IComparable where TDatabase : DbContext
{
    Task<bool> Insert(T entity);
    Task<ServiceResponse<T>> Insert<T>(T serviceEntity);
    Task<ServiceResponse<T>> UpdateById( ServiceResponse<T> serviceEntity, TId id, T inputEntity);
    Task<ServiceResponse<bool>> DeleteById( TId id);
    Task<ServiceResponse<T>> GetById( ServiceResponse<T> serviceEntity, TId id);
}