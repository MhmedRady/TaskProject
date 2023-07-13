using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class LoginResultDto
{
    public LoginResultDto() 
    {
        Roles = new List<string>();
    }
    public string? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Token { get; set; }
    public bool IsApproved { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public List<string> Roles { get; set; }
    public List<IdentityRoleClaim<string>> Claims { get; set; }

}
