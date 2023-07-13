using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;

namespace NewProject.Repositories;

public class UnitOfWork : IUnitOfWork
{
    MainDbContext _DBContext;
    public UnitOfWork(MainDbContext DBContext)
    {
        _DBContext = DBContext;
        PersonRepo = new PersonRepository(_DBContext);
        AddressRepo = new AddressRepository(_DBContext);
    }
    
    public IPersonRepository PersonRepo { get; private set; }
    public IAddressRepository AddressRepo { get; private set; }

    public async Task<int> SaveChangesAsync()
    {
        return await _DBContext.SaveChangesAsync();
    }
    
    public int SaveChanges()
    {
        return _DBContext.SaveChanges();
    }
    
    public void Dispose()
    {
       _DBContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
