namespace FilmRent.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Service;
    using Abstract;
    using Models;

    public class ActorRepository
        : Repository<Actor>, IActorRepository
    {
        #region Constructors 
        public ActorRepository(FilmRentContext context): base(context)
        {
        }

        public IEnumerable<Actor> SearchActors(string fistName)
        {
            return _context.Actors
                .Select(x => x)
                .Where(x => x.FirstName.Trim().Contains(fistName))
                .ToList();
        }
        #endregion //Constructors

        #region IActorRepository members

        public IEnumerable<Actor> SearchActors(string fistName, string lastName)
        {
            return _context.Actors
                .Select(x => x)
                .Where(x => x.FirstName.Trim().Contains(fistName) && lastName.Trim().Contains(lastName))
                .ToList();
        }
        #endregion //IActorRepository members

    }
}
