using Microsoft.AspNetCore.Identity;
using NewProject.Application;
using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories;

public interface IPersonRepository : IGeneralRepository<Person, string>
{
    
}


