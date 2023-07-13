using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain
{
    public class Person:IdentityUser
    {
        public string FullName { get; set; }
        public Guid AddressId { get; set; }
        [ForeignKey("Person Address")]
        public Address Address { get; set; }
    }
}
