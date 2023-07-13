using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace NewProject.Application;

public class CreateAddressInput
{
    public string? address { get; set; }
    public string City { get; set; }
}
