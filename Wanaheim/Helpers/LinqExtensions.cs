using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;

namespace Wanaheim.Helpers
{
    public static class LinqExtensions
    {
        public static IQueryable<T> MultipleInclude<T>(this IQueryable<T> data,
            Expression<Func<T, object>>[] includeValue) where T : class
        {
            if (includeValue != null)
            {
                foreach (var value in includeValue)
                {
                    data = data.Include(value);
                }
            }

            return data;
        }

        public static IQueryable<T> NullableWhere<T>(this IQueryable<T> data, Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                data = data.Where(filter).AsQueryable<T>();
            }

            return data;
        }

        public static IQueryable<T> NullableOrderBy<T>(this IQueryable<T> data, Dictionary<string, Expression<Func<T, object>>> columnsMap, ItemsQuery query)
        {
            if(String.IsNullOrWhiteSpace(query.SortBy) || !columnsMap.ContainsKey(query.SortBy))
            {
                return data;
            }

            if (query.IfAscending)
            {
                return data.OrderBy(columnsMap[query.SortBy]);
            }

            return data.OrderByDescending(columnsMap[query.SortBy]);
        }


        public static IQueryable<T> ApplingPagination<T>(this IQueryable<T> data, ItemsQuery query)
        {
            if(query.Page <= 0 )
            {
                query.Page = 1;
            }

            if(query.PageSize <= 0)
            {
                query.PageSize = 10;
            }

            return data
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);
        }
    }
}
