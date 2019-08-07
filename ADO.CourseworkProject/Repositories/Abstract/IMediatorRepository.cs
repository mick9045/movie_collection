namespace FilmRent.Repositories.Abstract
{
    using System;
    using EventArgs;

    public interface IMediatorRepository<TEntity> where TEntity : class
    {
        #region Events
        event EventHandler<NotifyRepositoryChangesArgs<TEntity>> RepositoryChanges;
        #endregion //Events
    }
}
