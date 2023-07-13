using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;

namespace NewProject.Repositories;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository PersonRepo { get; }
    IAddressRepository AddressRepo { get; }

    Task<int> SaveChangesAsync();
    int SaveChanges();
}
