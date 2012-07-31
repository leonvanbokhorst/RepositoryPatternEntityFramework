using System;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using Remondo.Model;

namespace Remondo.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected ObjectSet<T> DataTable;

        public Repository(ObjectContext dataContext)
        {
            DataTable = dataContext.CreateObjectSet<T>();
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            DataTable.AddObject(entity);
        }

        public void Delete(T entity)
        {
            DataTable.DeleteObject(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DataTable.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DataTable;
        }

public T GetById(int id)
{

    //return DataTable.Single(e => e.Id.Equals(id));

    var keyPropertyName = DataTable.EntitySet.ElementType
        .KeyMembers[0].ToString();

    return DataTable.Where("it." + keyPropertyName + "=" + id)
        .FirstOrDefault();
}

        #endregion
    }
}

