using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RBACV2.Application.Common.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string filters, CancellationToken cancellationToken) where T : class
        {
            if (string.IsNullOrEmpty(filters))
                return query;

            const string chars = @"(\s+|@|&|'|\(|\)|<|>|#|_|-)";

            var parameter = Expression.Parameter(typeof(T), "t");

            var expressions = new List<Expression>();

            foreach (var filter in filters.Split("&"))
            {
                var parts = filter.Split("=");
                if (parts.Length != 2) continue;

                var parameterObject = typeof(T).GetProperties()
                    .Where(t => Regex.Replace(t.Name.ToLower(), chars, "") == Regex.Replace(parts[0].ToLower(), chars, ""))
                    .Select(t => t.Name).FirstOrDefault();

                if (parameterObject is null) continue;

                var property = Expression.Property(parameter, parameterObject);

                if (property.Type == typeof(string) || property.Type == typeof(DateTime)
                    || property.Type == typeof(DateTimeOffset))
                {
                    var value = Expression.Constant(parts[1].ToLower());
                    var methodCallExpression = Expression
                   .Call(property, property.Type == typeof(string) ? "ToLower" : "ToString", null);
                    var call = Expression.Call(methodCallExpression, "Contains", null, value);

                    expressions.Add(call);
                }
                else if (property.Type.IsEnum)
                {
                    var enumValue = Enum.Parse(property.Type, parts[1], ignoreCase: true);
                    var enumString = Expression.Constant(enumValue);

                    expressions.Add(Expression.Equal(property, enumString));
                }
            }

            var body = expressions.Count > 0 ? expressions.Aggregate(Expression.Or) : Expression.Constant(true);

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            return query.Where(lambda);
        }
    }
}
