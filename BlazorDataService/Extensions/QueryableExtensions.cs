using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace BlazorDataService.Extensions
{
	public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T> (this IQueryable<T> query, string orderByMember, string direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
            typeof(Queryable),
            direction == "ASC" ? "OrderBy" : "OrderByDescending",
            new Type[] { typeof(T), memberAccess.Type },
            query.Expression,
            Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T> (orderBy);
        }
    }
}
