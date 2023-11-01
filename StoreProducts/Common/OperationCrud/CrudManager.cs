using AutoMapper;
using Common.Entity;
using Common.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Common.OperationCrud;

public class CrudManager<T, TId> : ICrudManager<T, TId> where T : BaseEntity<TId> where TId : struct, IComparable
{
    private readonly IMapper _mapper;

    public CrudManager(IMapper mapper)
    {
        _mapper = mapper;
    }

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

    public async Task<ServiceResponse<T>> UpdateById(DbContext dbContext, ServiceResponse<T> serviceEntity, TId id,T inputEntity)
    {
        var oldEntity = await GetById(dbContext, serviceEntity, id);
        var entity = _mapper.Map(inputEntity, oldEntity.Data);
        dbContext.Entry(entity).State = EntityState.Modified;
        var save = await dbContext.SaveChangesAsync() > 0;
        if (!save) return null;
        
        serviceEntity.Data = entity;
        return serviceEntity;
    }

    public async Task<ServiceResponse<T>> GetById(DbContext dbContext, ServiceResponse<T> serviceEntity, TId id)
    {
        var data = await dbContext.Set<T>().Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
        serviceEntity.Data = data;
        return serviceEntity.Data is not null ? serviceEntity : new ServiceResponse<T> { Data = null, Message = "No Data" };
    }


}

public interface ICrudManager<T, TId> where T : BaseEntity<TId> where TId : struct, IComparable
{
    Task<bool> Insert(DbContext dbContext, T entity);
    Task<ServiceResponse<T>> Insert(DbContext dbContext, ServiceResponse<T> serviceEntity);
    Task<ServiceResponse<T>> UpdateById(DbContext dbContext, ServiceResponse<T> serviceEntity, TId id, T inputEntity);
    Task<ServiceResponse<T>> GetById(DbContext dbContext, ServiceResponse<T> serviceEntity, TId id);
}