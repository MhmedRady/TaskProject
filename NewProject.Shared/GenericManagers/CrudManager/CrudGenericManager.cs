using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Shared;

public class CrudGenericManager<TKey, Entity, ReadDto, WriteDto> : ICrudGenericManager<TKey, Entity, ReadDto, WriteDto> 
    where ReadDto : class 
    where WriteDto : class
    where Entity : class
{
    public ReadDto Add(WriteDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ReadDto> AddAsync(WriteDto dto)
    {
        throw new NotImplementedException();
    }

    public int Count(Expression<Func<Entity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReadDto>> GetAll(Expression<Func<Entity, bool>> expression, int? take, int? skip, Expression<Func<Entity, object>> orderBy, string orderDirection = "ASC", params string[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ReadDto>>  GetAll(Expression<Func<Entity, bool>> expression, params string[] includes)
    {
        throw new NotImplementedException();
    }

    public ReadDto GetBy(Expression<Func<Entity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<ReadDto> GetById(TKey Id)
    {
        throw new NotImplementedException();
    }

    public Task<Entity> GetModelById(TKey Id)
    {
        throw new NotImplementedException();
    }

    public bool IsExisted(Expression<Func<Entity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistedAsync(Expression<Func<Entity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Entity entity)
    {
        throw new NotImplementedException();
    }

    public Task<ReadDto> Update(WriteDto dto, TKey id)
    {
        throw new NotImplementedException();
    }

}
