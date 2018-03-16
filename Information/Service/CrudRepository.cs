using Information.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Information.Service
{
    public class CrudRepository<TEntity>
        where TEntity : class
    {
        private DbContext Db { get; set; }

        public CrudRepository() : this(new AppDbContext())
        {
        }
        public CrudRepository(DbContext db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("db");
            }
            Db = db;
        }
        public CrudRepository(ObjectContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            Db = new DbContext(db, true);
        }

        public void Create(TEntity entity)
        {
            
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Set<TEntity>().Add(entity);
                SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Entry(entity).State = EntityState.Modified;
                SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Db.Entry(entity).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().FirstOrDefault(predicate);
        }


        public IQueryable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Db != null)
                {
                    Db.Dispose();
                    Db = null;
                }
            }
        }
    }
}