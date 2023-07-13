using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories
{
    public interface IGeneralRepository<T, TKey> where T : class
    {
        EntityEntry<T> Add(T entry);
        Task<EntityEntry<T>> AddAsync(T entry);
        IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderby = null, string? orderbyDirection = Constanties.ORDERASC, int? take = null, int? skip = null, params string[] include);
        IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, params string[] include);
        T? GetBy(Expression<Func<T,bool>> expression);
        T? GetBy(Expression<Func<T,bool>> expression,params string[] include);
        EntityEntry<T> Remove(T entry);
        EntityEntry<T> Update(T entry);
        Task<T?> GetById(TKey Id);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression);
        bool IsExisted(Expression<Func<T, bool>> expression);
        int Count(Expression<Func<T, bool>>? expression = null);
    }
}
