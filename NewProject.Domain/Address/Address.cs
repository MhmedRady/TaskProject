using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain
{
    public class Address : BaseEntity<Guid>
    {
        public string? address { get; set; }
        public string? City { get; set; }
        public Person Person { get; set; }
    }
}
