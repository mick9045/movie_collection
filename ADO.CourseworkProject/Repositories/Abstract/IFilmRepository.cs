namespace FilmRent.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface IFilmRepository
        :IRepository<Film>
    {
        IEnumerable<Film> GetFilmsByTitle(string title);
        IEnumerable<Film> SearchFilmsByTitle(string title);
        Film GetFilmMapped(Film film);
        IEnumerable<Film> GetFilmsByActor(string actor);
    }
}
