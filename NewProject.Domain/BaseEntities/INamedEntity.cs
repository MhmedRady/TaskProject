using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain;

public interface INamedEntity
{
    string ArabicName { get; set; }
    string EnglishName { get; set; }
}
