using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain;

public interface ICreationalTime
{
    DateTime CreatedAt { get; set; } 
    DateTime? UpdateAt { get; set; }
}
