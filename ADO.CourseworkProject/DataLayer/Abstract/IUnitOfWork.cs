namespace FilmRent.DataLayer.Abstract
{
    using System;
    using Repositories.Abstract;
    using Repositories;

    interface IUnitOfWork
        : IDisposable
    {
        AccountRepository Accounts { get; }
        CountryRepository Countries { get; }
        GenreRepository Genres { get; }
        FilmRepository Films { get; }
        ActorRepository Actors { get; }

        void SaveChanges();
    }
}
