using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class AddressDto
{
    public string Id { get; set; }
    public string? address { get; set; }
    public string? City { get; set; }
    public PersonDto Person { get; set; }
}
