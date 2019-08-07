namespace FilmRent.DataLayer.Abstract
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        event Action<T> ItemCreated;
        event Action<T> ItemUpdated;
        event Action<T> ItemDeleted;
    }
}
