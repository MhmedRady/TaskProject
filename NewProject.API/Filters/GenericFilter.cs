using System.Linq.Expressions;
using System.Reflection;

namespace NewProject.Application.Filters;

public class GenericFilter<T> where T: class 
{
    public Dictionary<string, string> GetFilterExpression(FilterModel filterModel)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach(PropertyInfo pi in filterModel.GetType().GetProperties())
        {
            if(pi.PropertyType == typeof(string))
            {
                string v = (string)pi.GetValue(filterModel);
                if(string.IsNullOrEmpty(v))
                {
                    dic.Add(pi.Name, v);
                }
            }
        }

        return dic;
    }
}