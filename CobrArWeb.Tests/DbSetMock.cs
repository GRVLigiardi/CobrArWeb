using Moq;
using Moq.Language.Flow;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CobrArWeb.Tests
{
    public static class DbSetMock
    {
        public static ISetup<T, DbSet<TItem>> ReturnsDbSet<T, TItem>(this ISetup<T, DbSet<TItem>> setup, IEnumerable<TItem> items) where T : class where TItem : class
        {
            var queryable = items.AsQueryable();
            var dbSet = new Mock<DbSet<TItem>>();

            dbSet.As<IQueryable<TItem>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<TItem>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<TItem>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<TItem>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            setup.Returns(dbSet.Object);

            return setup;
        }
    }
}
