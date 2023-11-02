using Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Database;

public static class ModelBuilderExtensions
{
    public static void RegisterAllEntities<T>(this ModelBuilder builder)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(s => s.IsClass && !s.IsAbstract && s.IsPublic && s.IsSubclassOf(typeof(T)));

        foreach (var type in types)
            builder.Entity(type);
    }

    public static void SoftDeleteGlobalFilter(this ModelBuilder modelBuilder)
    {
        Expression<Func<BaseEntity<int>, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!mutableEntityType.ClrType.IsAssignableTo(typeof(BaseEntity<int>))) continue;
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);
            mutableEntityType.SetQueryFilter(lambdaExpression);
        }
    }
}