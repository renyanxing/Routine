using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Routine.Api.Services;

namespace Routine.Api.Helpers
{
    public static class IQueryableExtension
    {

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }
            var orderByAfterSplit = orderBy.Split(',');
            string orderString = string.Empty;
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1
                    ? trimmedOrderByClause
                    : trimmedOrderByClause.Remove(indexOfFirstSpace);
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentNullException($"没有找到Key为{propertyName}的映射关系");
                }

                var propertyMappingValue = mappingDictionary[propertyName];
                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException(nameof(propertyMappingValue));
                }

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    orderString = orderString + destinationProperty + (orderDescending ? " descending," : " ascending,");
                        //source.OrderBy(destinationProperty + (orderDescending ? " desc" : " asc"));
                }
                
            }
            orderString=orderString.Substring(0,orderString.Length - 1);
            source = source.OrderBy(orderString);
            return source;
        }
    }
}
