namespace FilmRent.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    public interface IActorRepository
        : IRepository<Actor>
    {
        IEnumerable<Actor> SearchActors(string fistName, string lastName);
        IEnumerable<Actor> SearchActors(string fistName);
    }
}
