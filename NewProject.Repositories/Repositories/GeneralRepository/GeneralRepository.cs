using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using NewProject.Domain;
using NewProject.Domain.BaseEntities;
using NewProject.EntityFrameworkCore;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories
{
    public class GeneralRepository<T, TKey> :IGeneralRepository<T, TKey> where T : class
    {
        private readonly MainDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public GeneralRepository(MainDbContext _DBContext)
        {
            _dbContext = _DBContext;
        }
        
        public GeneralRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private IQueryable<T> SetEntity()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderby = null, string? orderbyDirection = Constanties.ORDERASC , int? take = null, int? skip = null, params string[] include)
        {
            var query = this.SetEntity();
            if (include != null && include.Length > 0)
                foreach (string i in include)
                    query = query.Include(i);
            if (expression != null)
                query = query.Where(expression);
            
            if (take.HasValue)
                query.Take(take.Value);

            if (skip.HasValue)
                query.Skip(skip.Value);
            
            if (orderby is not null)
            {
                if(orderbyDirection == Constanties.ORDERASC)
                {
                    query = query.OrderBy(orderby);
                }
                else
                {
                    query = query.OrderByDescending(orderby);
                }
            }
            return query;
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null, params string[] include)
        {
            return this.Get(expression, null, null, null, include: include);
        }
        public T? GetBy(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return query.Where(expression).FirstOrDefault();
        }
        public async Task<T?> GetById(TKey Id)
        {
            return  await _dbContext.Set<T>().FindAsync(Id);
        }
        
        public async Task<EntityEntry<T>> AddAsync(T entry) => await _dbContext.Set<T>().AddAsync(entry);
        public EntityEntry<T> Add(T entry) => _dbContext.Set<T>().Add(entry);
        public EntityEntry<T> Update(T entry) => _dbContext.Set<T>().Update(entry);
        public EntityEntry<T> Remove(T entry) => _dbContext.Set<T>().Remove(entry);

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public bool IsExisted(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return query.Any(expression);
        }
        public async Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression)
        {
            var query = this.SetEntity();
            return await query.AnyAsync(expression);
        }
        public int Count(Expression<Func<T, bool>>? expression)
        {
            var query = this.SetEntity();
            if(expression == null)
                return query.Count();
            return query.Count(expression);
        }

        public T? GetBy(Expression<Func<T, bool>> expression, params string[] include)
        {
            return Get(expression, include).FirstOrDefault();
        }
    }
}
