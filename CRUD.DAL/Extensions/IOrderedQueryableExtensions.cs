using CRUD.DTO.Enums.Sort;
using System.Linq.Expressions;


namespace CRUD.DAL.Repositories.Extensions
{
    public static class IOrderedQueryableExtensions
    {
        public static IOrderedQueryable<TEntity> ThenByWithSortDirection<TEntity>
                (this IOrderedQueryable<TEntity> source,
                Expression<Func<TEntity, object>> keySelector,
                SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Asc)
            {
                return source.ThenBy(keySelector);
            }

            return source.ThenByDescending(keySelector);
        }

        public static IOrderedQueryable<TEntity> OrderByWithSortDirection<TEntity>
        (this IOrderedQueryable<TEntity> source,
        Expression<Func<TEntity, object>> keySelector,
        SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Asc)
            {
                return source.OrderBy(keySelector);
            }

            return source.OrderByDescending(keySelector);
        }
    }



}
