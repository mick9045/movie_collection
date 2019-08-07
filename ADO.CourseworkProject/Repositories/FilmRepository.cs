namespace FilmRent.Repositories
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Abstract;
    using System.Linq;
    using System.Linq.Expressions;

    public class FilmRepository
        : Repository<Film>, IFilmRepository
    {
        #region Constructors
        public FilmRepository(FilmRentContext context)
            : base(context)
        {
        }
        #endregion //Constructors

        #region IFilmRepository members
        public Film GetFilmMapped(Film film)
        {
            return _context.Films
                .Include("Countries")
                .Include("Genres")
                .Include("Actors")
                .Select(x => x)
                .Where(x => x.IdFilm == film.IdFilm)
                .FirstOrDefault();
        }
        
        public IEnumerable<Film> GetFilmsByActor(string actor)
        {
            string[] parts = actor.ToLower().Split(' ');
            throw new NotImplementedException();
            /*
            if (parts.Length >= 2)
            {
                return _context.Films
                    .Select(x => x)
                    .Where(x => x.Acotr.Name == parts[0] && x=>)
                    .ToList();
            }
            else
            {
                return _context.Films
                    .Select(x => x)
                    .Where(x => x.Name.)
            }
            */
            return new List<Film>();

        }

        public IEnumerable<Film> GetFilmsByTitle(string title)
        {
            return _context.Films
                .Select(x => x)
                .Where(x => x.Name.Trim().ToLower() == title.ToLower())
                .ToList();
        }

        public IEnumerable<Film> SearchFilmsByTitle(string title)
        {
            return _context.Films
               .Select(x => x)
               .Where(x => x.Name.Trim().ToLower().Contains(title.Trim().ToLower()))
               .ToList();
        }
        #endregion //IFilmRepository members
    }
}
