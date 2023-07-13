using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;

namespace NewProject.Application;

public class PersonDto 
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    /*public string address
    {
        get => Address.address;
    }

    public string City
    {
        get => Address.City;
    }*/
    
    public AddressDto Address {get; private set; }
}
