namespace FilmRent.Repositories
{
    using System;
    using Abstract;
    using FilmRent.EventArgs;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Models;

    public class Repository<TEntity>
        : IRepository<TEntity>, IMediatorRepository<TEntity> where TEntity : class
    {
        #region Fields
        protected readonly FilmRentContext _context;
        #endregion //Fields

        #region Constructors
        public Repository(FilmRentContext context)
        {
            _context = context;
        }
        #endregion //Constructors

        #region IMediatorRepository members
        public event EventHandler<NotifyRepositoryChangesArgs<TEntity>> RepositoryChanges;
        #endregion //IMediatorRepository members

        #region IRepositoryMembers
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            RepositoryChanges?.Invoke(
                this,
                new NotifyRepositoryChangesArgs<TEntity>(
                    RepositoryAction.Add,
                    entity
                    )
                );
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            RepositoryChanges?.Invoke(
                this,
                new NotifyRepositoryChangesArgs<TEntity>(
                    RepositoryAction.Add,
                    entities
                    )
                );
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            RepositoryChanges?.Invoke(
                this,
                new NotifyRepositoryChangesArgs<TEntity>(
                    RepositoryAction.Remove,
                    entity
                    )
                );
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            RepositoryChanges?.Invoke(
                this,
                new NotifyRepositoryChangesArgs<TEntity>(
                    RepositoryAction.Remove,
                    entities
                    )
                );
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            RepositoryChanges?.Invoke(
                this,
                new NotifyRepositoryChangesArgs<TEntity>(
                    RepositoryAction.Update,
                    entity
                    )
                );
        }
        #endregion //IRepository Members
    }
}
