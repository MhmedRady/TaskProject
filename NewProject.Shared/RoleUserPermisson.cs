using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class RoleUserPermisson
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public List<string> Permissions { get; set; }
}
