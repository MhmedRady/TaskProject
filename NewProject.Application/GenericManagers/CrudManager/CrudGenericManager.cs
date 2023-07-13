using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NewProject.Domain.BaseEntities;
using NewProject.Repositories;
using NewProject.Shared;

namespace NewProject.Application;

public class CrudGenericManager<TKey, Entity, ReadDto, WriteDto> : ICrudGenericManager<TKey, Entity, ReadDto, WriteDto> 
    where ReadDto : class 
    where WriteDto : class
    where Entity : Entity<TKey>
{
    private readonly IGeneralRepository<Entity, TKey> _repository;
    private readonly IMapper _mapper;
    public IMapper Mapper { get; }

    public CrudGenericManager(IGeneralRepository<Entity, TKey> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public ReadDto Add(WriteDto dto)
    {
        var entity = _mapper.Map<WriteDto, Entity>(dto);
        _repository.Add(entity);
        _repository.SaveChanges();
        return _mapper.Map<ReadDto>(entity);
    }

    public async Task<ReadDto> Update(WriteDto dto, TKey id)
    {
        var obj = _mapper.Map<Entity>(dto);
        obj.Id = id;
        var result = _repository.Update(obj);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ReadDto>(result.Entity);
    }

    public async Task<bool> Remove(TKey id)
    {
        var e = await _repository.GetById(id);
        if (e is not null)
        {
            var entity = _mapper.Map<Entity>(e);
            _repository.Remove(entity);
            _repository.SaveChanges();
        }
        return e is not null;
    }

    public async Task<ReadDto> AddAsync(WriteDto dto)
    {
        var entity = _mapper.Map<WriteDto, Entity>(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ReadDto>(entity);
    }

    public int Count(Expression<Func<Entity, bool>> expression)
    {
        return _repository.Count(expression);
    }

    public async Task<IEnumerable<ReadDto>> GetAll(Expression<Func<Entity, bool>> expression,  int? take, int? skip, 
        Expression<Func<Entity, object>> orderBy, string orderDirection = Constanties.ORDERASC, params string[] includes)
    {
        var data = _repository.Get(expression: expression, take: take, skip: skip, orderby: orderBy,
            orderbyDirection: orderDirection, include: includes);
        return _mapper.Map<IEnumerable<ReadDto>>(data);
    }

    public async Task<IEnumerable<ReadDto>>  GetAll(Expression<Func<Entity, bool>> expression, params string[] includes)
    {
        var data = _repository.Get(expression: expression, include: includes);
        return _mapper.Map<IEnumerable<ReadDto>>(data);
    }

    public ReadDto GetBy(Expression<Func<Entity, bool>> expression)
    {
        var obj = _repository.GetBy(expression);
        return _mapper.Map<ReadDto>(obj);
    }

    public async Task<ReadDto> GetById(TKey Id)
    {
        var obj = await _repository.GetById(Id);
        return _mapper.Map<ReadDto>(obj);
    }

    public async Task<Entity> GetModelById(TKey Id)
    {
        return await _repository.GetById(Id);
    }

    public bool IsExisted(Expression<Func<Entity, bool>> expression)
    {
        return _repository.IsExisted(expression);
    }

    public async Task<bool> IsExistedAsync(Expression<Func<Entity, bool>> expression)
    {
        return await _repository.IsExistedAsync(expression);
    }

}
