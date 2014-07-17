using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public static class Exstensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string sortExpression)
        {
            string[] sortExpressionSegments = sortExpression.Split(' ');

            return collection.OrderBy<T>(sortExpressionSegments[0].Trim(), sortExpressionSegments[1].ToUpperInvariant().Trim() == "ASC");
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string sortingExpression, SortDirection sortDirection)
        {
            return collection.OrderBy<T>(sortingExpression.Trim(), sortDirection == SortDirection.Ascending);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string sortField, bool ascending)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            MemberExpression prop = Expression.Property(param, sortField);
            LambdaExpression exp = Expression.Lambda(prop, param);
            string method = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { collection.ElementType, exp.Body.Type };
            MethodCallExpression mce = Expression.Call(typeof(Queryable), method, types, collection.Expression, exp);

            return collection.Provider.CreateQuery<T>(mce);
        }

        public static IQueryable<T> OrderBy<T>(this IEnumerable<T> collection, string sortField, bool ascending)
        {
            return collection.AsQueryable<T>().OrderBy(sortField, ascending);
        }

        public static IQueryable<T> OrderBy<T>(this IEnumerable<T> collection, string sortField, SortDirection sortDirection)
        {
            return collection.AsQueryable<T>().OrderBy(sortField, sortDirection);
        }
    }
}