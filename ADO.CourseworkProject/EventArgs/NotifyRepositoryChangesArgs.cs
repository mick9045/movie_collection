namespace FilmRent.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum RepositoryAction
    {
        Update, Add, Remove
    }

    public class NotifyRepositoryChangesArgs<TEntity>
        : EventArgs where TEntity : class
        
    {
        #region Fields
        RepositoryAction _action;
        IEnumerable<TEntity> _items;
        #endregion //Fields

        #region Constructors
        public NotifyRepositoryChangesArgs(RepositoryAction action, IEnumerable<TEntity> items)
        {
            Action = action;
            Items = items;
        }

        public NotifyRepositoryChangesArgs(RepositoryAction action, TEntity item)
        {
            Action = action;
            var list = new List<TEntity>();
            list.Add(item);
            Items = list;
        }
        #endregion //Constructors

        #region Properties
        public IEnumerable<TEntity> Items
        {
            get { return _items; }
            private set { _items = value; }
        }

        public RepositoryAction Action
        {
            get { return _action; }
            private set { _action = value; }
        }
        #endregion //Properties
    }
}
