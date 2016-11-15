using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WSS.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        protected WssApplicationContext Context;
        protected DbSet<T> DbSet;

        public DataRepository(WssApplicationContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        protected internal DataRepository()
        {
            //Constructor for testing only!!!
        }

        public virtual IQueryable<T> FindAll()
        {
            return DbSet ?? new List<T>() as IQueryable<T>;
        }

        public virtual T Get(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            SetActiveDefault(entity);	// defaults IsActive to true, we need this because the default for booleans is false
            DbSet.Add(entity);			// and if we forget to set the IsActive to true (guaranteed to happen) the inserted entity
        }								// won't be active.

        private void SetActiveDefault(T entity)
        {
            var prop = typeof(T).GetProperty("IsActive");
            if (prop != null)
            {
                prop.SetValue(entity, true);
            }
        }

        public virtual void Delete(T entity)
        {
            //var prop = typeof(T).GetProperty("IsActive");
            //if (prop != null)
            //{
            //    prop.SetValue(entity, false);
            //}
            //else
            //{
            //    if (Context.Entry(entity).State == EntityState.Detached)
            //    {
            //        DbSet.Attach(entity);
            //    }
            //    DbSet.Remove(entity);
            //}

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }
    }
}