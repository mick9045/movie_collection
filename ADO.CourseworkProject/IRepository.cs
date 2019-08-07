using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
        public interface IRepository<T> where T : IEntity
        {
            IEnumerable<T> GetList();
            T GetItem(int id);
            void Create(T item);
            void Update(T item);
            void Delete(int id);
        }
}
