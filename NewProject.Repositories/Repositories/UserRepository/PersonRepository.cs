using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Application;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories;

public class PersonRepository : GeneralRepository<Person, string>, IPersonRepository
{
    MainDbContext _DBContext;
    public PersonRepository(MainDbContext DBContext) : base(DBContext)
    {
        _DBContext = DBContext;
    }

    public async Task<List<IdentityRoleClaim<string>>> GetUserPermisson(string userid)
    {
        var claims = new List<IdentityRoleClaim<string>>();
        var roles = _DBContext.UserRoles.Where(u => u.UserId == userid).ToList();
        foreach (var role in roles)
        {
            var claim = await _DBContext.RoleClaims.Where(rc => rc.RoleId == role.RoleId)
                .ToListAsync();
            if (claim != null)
            {
                claims.AddRange(claim);
            }
        }
        return claims;

    }
}
