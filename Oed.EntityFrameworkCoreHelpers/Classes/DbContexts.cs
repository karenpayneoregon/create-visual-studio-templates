using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oed.EntityFrameworkCoreHelpers.Classes
{
    public static class DbContexts
    {
        /// <summary>
        /// Find by one or more keys
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="dbContext">DbContext</param>
        /// <param name="keyValues">One or more keys</param>
        /// <code>
        /// await using var context = new NorthWindContext();
        /// var keys = new object[] {1, 2, 3, 4};
        /// var results = await context.FindAllAsync&lt;Category&gt;(keys);
        /// </code>
        public static Task<T[]> FindAllAsync<T>(this DbContext dbContext, params object[] keyValues) where T : class
        {
            var entityType = dbContext.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey.Properties.Count != 1)
            {
                throw new NotSupportedException("Only a single primary key is supported");
            }

            var pkProperty = primaryKey.Properties[0];
            var pkPropertyType = pkProperty.ClrType;

            foreach (var keyValue in keyValues)
            {
                if (!pkPropertyType.IsInstanceOfType(keyValue))
                {
                    throw new ArgumentException($"Key value '{keyValue}' is not of the right type");
                }
            }

            var pkMemberInfo = typeof(T).GetProperty(pkProperty.Name);

            if (pkMemberInfo is null)
            {
                throw new ArgumentException("Type does not contain the primary key as an accessible property");
            }


            var parameter = Expression.Parameter(typeof(T), "e");
            var body = Expression.Call(null, ContainsMethod,
                Expression.Constant(keyValues),
                Expression.Convert(Expression.MakeMemberAccess(parameter, pkMemberInfo), typeof(object)));
            var predicateExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            return dbContext.Set<T>().Where(predicateExpression).ToArrayAsync();
        }

        private static readonly MethodInfo ContainsMethod = typeof(Enumerable)
            .GetMethods()
            .FirstOrDefault(methodInfo => methodInfo.Name == "Contains" && methodInfo.GetParameters().Length == 2)?.MakeGenericMethod(typeof(object));
    }

}