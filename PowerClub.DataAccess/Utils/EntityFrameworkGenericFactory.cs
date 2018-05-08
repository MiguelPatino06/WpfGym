using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PowerClub.DataAccess.Utils
{
    public class EntityFrameworkGenericFactory<T, TKey> : IGenericFactoryAsync<T, TKey> where T : class
    {
        #region Properties

        private bool AlwaysCommit { get; set; }

        private DbContext Context { get; set; }

        public bool IsDisposed { get; private set; }

        #endregion

        #region Constructors

        public EntityFrameworkGenericFactory(DbContext context, bool commit = false)
        {
            Context = context;
            AlwaysCommit = commit;
        }

        public EntityFrameworkGenericFactory(bool commit = true)
            : this(null, commit)
        {
            AlwaysCommit = commit;
        }

        ~EntityFrameworkGenericFactory()
        {
            Dispose(false);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }

            IsDisposed = true;
        }

        #endregion

        #region Core

        protected virtual void SaveCore(T entity)
        {
            var db = GetContext();
            var dbset = db.Set<T>();

            dbset.Add(entity);
        }

        protected virtual void UpdateCore(T entity, bool async)
        {
            var db = GetContext();
            db.Entry(entity).State = EntityState.Modified;

            if (AlwaysCommit)
            {
                if (async)
                {
                    CommitAsync();
                }
                else
                {
                    Commit();
                }
            }
        }

        protected virtual void DeleteCore(T entity, bool async)
        {
            var db = GetContext();
            try
            {
                var dbset = db.Set<T>();
                dbset.Remove(entity);

                if (AlwaysCommit)
                {
                    if (async)
                    {
                        CommitAsync();
                    }
                    else
                    {
                        Commit();
                    }
                }
            }
            catch (Exception)
            {
                if (db.ChangeTracker.Entries().Any(q => q.Entity.Equals(entity) && q.State == EntityState.Deleted))
                {
                    db.Entry(entity).State = EntityState.Unchanged;
                }

                throw;
            }
        }

        protected virtual IQueryable<TR> QueryCore<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties,
            params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            var db = GetContext();
            var dbset = db.Set<T>();

            var source = dbset.OfType<TR>();
            if (includeProperties != null)
            {
                var expressions = includeProperties as Expression<Func<TR, object>>[] ?? includeProperties.ToArray();
                if (!CollectionUtils.IsNullOrEmpty(expressions))
                {
                    source = PerformInclusions(expressions, source);
                }
            }

            if (!CollectionUtils.IsNullOrEmpty(filters))
            {
                source = filters.Aggregate(source, (current, expression) => current.Where(expression));
            }

            return source;
        }

        #endregion

        #region Helper

        private static IQueryable<TR> PerformInclusions<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties,
            IQueryable<TR> query) where TR : class, T
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion

        #region Context Management

        #region Sync

        protected virtual DbContext GetContext()
        {
            return Context;
        }

        public virtual int Commit()
        {
            return GetContext().SaveChanges();
        }

        #endregion

        #region Async

        public virtual Task<int> CommitAsync()
        {
            return GetContext().SaveChangesAsync();
        }

        #endregion

        #endregion

        #region CRUD Operations

        #region Sync

        public void Save(T entity)
        {
            SaveCore(entity);
            if (AlwaysCommit)
            {
                Commit();
            }
        }

        public void Update(T entity)
        {
            UpdateCore(entity, false);
        }

        public void Delete(T entity)
        {
            DeleteCore(entity, false);
        }

        public void Delete(params Expression<Func<T, bool>>[] filters)
        {
            var db = GetContext();
            var dbset = db.Set<T>();

            var objects = !CollectionUtils.IsNullOrEmpty(filters)
                ? filters.Aggregate(dbset.OfType<T>(), (current, expression) => current.Where(expression))
                : dbset.AsQueryable();

            foreach (var obj in objects)
            {
                DeleteCore(obj, true);
            }
        }

        #endregion

        #region Async

        public async Task SaveAsync(T entity)
        {
            SaveCore(entity);
            if (AlwaysCommit)
            {
              await CommitAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            await  Task.Factory.StartNew(() => UpdateCore(entity, true));
        }

        public async Task DeleteAsync(T entity)
        {
          await  DeleteCore(entity, true);
        }

        #endregion

        #endregion

        #region Query Operations

        #region Sync

        public T GetById(TKey id)
        {
            var db = GetContext();
            return db.Set<T>().Find(id);
        }

        public T GetByIdGuid(Guid id)
        {
            var db = GetContext();
            return db.Set<T>().Find(id);
        }


        public TR First<TR>(params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return QueryCore(null, filters).FirstOrDefault();
        }

        public T First(params Expression<Func<T, bool>>[] filters)
        {
            return First<T>(filters);
        }


        public T First(IEnumerable<Expression<Func<T, object>>> includeProperties, params Expression<Func<T, bool>>[] filters)
        {
            return QueryCore(includeProperties, filters).FirstOrDefault();
        }

        public TR First<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties, params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return QueryCore(includeProperties, filters).FirstOrDefault();
        }

        public IQueryable<TR> GetAll<TR>(params Expression<Func<TR, object>>[] includeProperties) where TR : class, T
        {
            return QueryCore(includeProperties);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAll<T>(includeProperties);
        }

        public IQueryable<TR> Find<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties,
            params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return QueryCore(includeProperties, filters);
        }

        public IQueryable<TR> Find<TR>(params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return Find(null, filters);
        }

        public IQueryable<T> Find(IEnumerable<Expression<Func<T, object>>> includeProperties,
            params Expression<Func<T, bool>>[] filters)
        {
            return Find<T>(includeProperties, filters);
        }

        public IQueryable<T> Find(params Expression<Func<T, bool>>[] filters)
        {
            return Find<T>(filters);
        }

        public int Count(params Expression<Func<T, bool>>[] filters)
        {
            return Find(filters).Count();
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> source = GetContext().Set<T>();
            return includeProperties.Aggregate(source, (current, path) => current.Include(path));
        }

        #endregion

        #region Async

        public Task<T> GetByIdAsync(TKey id)
        {
            var db = GetContext();
            return db.Set<T>().FindAsync(id);
        }

        public Task<TR> FirstAsync<TR>(params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return QueryCore(null, filters).FirstOrDefaultAsync();
        }

        public Task<T> FirstAsync(params Expression<Func<T, bool>>[] filters)
        {
            return FirstAsync<T>(filters);
        }

        public Task<T> FirstAsync(IEnumerable<Expression<Func<T, object>>> includeProperties, params Expression<Func<T, bool>>[] filters)
        {
            return FirstAsync<T>(includeProperties, filters);
        }

        public Task<TR> FirstAsync<TR>(IEnumerable<Expression<Func<TR, object>>> includeProperties, params Expression<Func<TR, bool>>[] filters) where TR : class, T
        {
            return QueryCore(includeProperties, filters).FirstOrDefaultAsync();
        }

        public Task<int> CountAsync(params Expression<Func<T, bool>>[] filters)
        {
            return QueryCore(null, filters).CountAsync();
        }

        #endregion

        #endregion
    }
}
