using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using NewProject.Domain.BaseEntities;
using System.Linq.Expressions;

namespace NewProject.Application;

public interface IPersonManager
{
    Task<OperationResult> ChangePasswordAsync(string PersonId, string currentPassword, string newPassword);
    Task<LoginResultDto> Login(LoginDto model);
    public Task<IEnumerable<PersonDto>> GetAll(Expression<Func<Person, bool>> expression, int? take, int? skip,
    Expression<Func<Person, object>> orderBy,
    string orderDirection = Constanties.ORDERASC, params string[] includes);
    public Task<IEnumerable<PersonDto>> GetAll(Expression<Func<Person, bool>> expression, params string[] includes);
    public PersonDto GetBy(Expression<Func<Person, bool>> expression);
    public Task<PersonDto> GetById(string Id);
    public Task<Person> GetModelById(string Id);
    public Task<PersonDto> AddAsync(CreatePersonInput dto);
    public PersonDto Add(CreatePersonInput dto);
    public Task<PersonDto> Update(CreatePersonInput dto, string id);
    public bool Remove(Person entity);
    Task<bool> IsExistedAsync(Expression<Func<Person, bool>> expression);
    bool IsExisted(Expression<Func<Person, bool>> expression);
    int Count(Expression<Func<Person, bool>> expression);
    }
