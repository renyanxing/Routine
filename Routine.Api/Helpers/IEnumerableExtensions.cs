using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Routine.Api.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<T>(this IEnumerable<T> source ,string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var expandoObjectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                propertyInfoList.AddRange(typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance));
                
            }
            else
            {
                var fieldsAfterSplit = fields.Split(',');
                fieldsAfterSplit.Select(x =>
                {
                    var propertyName = x.Trim();
                    var propertyInfo = typeof(T).GetProperty(x, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property:{propertyName}没有找到:{typeof(T)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                    return true;
                });
                
            }
            foreach (T item in source)
            {
                var shapeObj = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(item);
                    ((IDictionary<string, object>)shapeObj).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(shapeObj);
            }
            return expandoObjectList;
        }
    }
}
