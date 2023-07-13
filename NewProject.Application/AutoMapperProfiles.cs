using AutoMapper;
using NewProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class AutoMapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonInput, Person>();
            CreateMap<Person, PersonDto>();
        }
    }
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressInput, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}