using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Application;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories;

public class AddressRepository : GeneralRepository<Address, Guid>, IAddressRepository
{
    MainDbContext _DBContext;
    
    public AddressRepository(MainDbContext DBContext) : base(DBContext)
    {
        _DBContext = DBContext;
    }

    public IQueryable<Address> GetAddressInCity()
    {
        Expression<Func<Address, bool>> where = address => address.address == "nasr city"; 
        return this.Get(where);
    }
}
