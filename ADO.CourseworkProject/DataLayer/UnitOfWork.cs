namespace FilmRent.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositories;
    using Repositories.Abstract;
    using Abstract;
    using Models;

    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private FilmRentContext _context = new FilmRentContext();
        private AccountRepository _accountRepository;
        private GenreRepository _genreRepository;
        private CountryRepository _countryRepository;
        private FilmRepository _filmRepository;
        private ActorRepository _actorRepository;
        #endregion //Fields

        #region Constructors
        public UnitOfWork()
        {
            _accountRepository = new AccountRepository(_context);
            _genreRepository = new GenreRepository(_context);
            _countryRepository = new CountryRepository(_context);
            _filmRepository = new FilmRepository(_context);
            _actorRepository = new ActorRepository(_context);
        }
        #endregion //Constructors

        #region IUnitOfWork Members
        public ActorRepository Actors
        {
            get { return _actorRepository; }
        }

        public AccountRepository Accounts
        {
            get { return _accountRepository; }
        }

        public CountryRepository Countries
        {
            get { return _countryRepository; }
        }

        public GenreRepository Genres
        {
            get { return _genreRepository; }
        }
        
        public FilmRepository Films
        {
            get { return _filmRepository; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion //IUnitOfWork Members
    }
}
