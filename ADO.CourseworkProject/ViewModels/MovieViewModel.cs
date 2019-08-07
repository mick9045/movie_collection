namespace FilmRent.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ViewModels.Abstract;
    using System.Threading.Tasks;
    using Models;
    using EventArgs;

    public class MovieViewModel
        : WorkspaceViewModel
    {
        #region Fields
        private Film _film;
        #endregion //Fields

        #region Constructors
        public MovieViewModel(string displayName, Film film)
            : base(displayName)
        {
            if (film == null)
            {
                throw new ArgumentNullException("film");
            }
            _film = unitOfWork.Films.GetFilmMapped(film);
            unitOfWork.Films.RepositoryChanges += Films_RepositoryChanges;
            DisplayName = displayName;
            _film = film;
        }

        private void Films_RepositoryChanges(object sender, NotifyRepositoryChangesArgs<Film> e)
        {
            if (e.Action == RepositoryAction.Remove && e.Items.FirstOrDefault() == _film)
            {
                CloseCommand.Execute(this);
            }
        }
        #endregion //Constructors

        #region Properties
        public IEnumerable<Country> MovieCountries
        {
            get { return _film.Countries; }
        }

        public IEnumerable<Genre> MovieGenres
        {
            get { return _film.Genres; }
        }

        public string ImagePath
        {
            get { return _film.ImagePath; }
            private set
            {
                _film.ImagePath = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Actor> MovieActors
        {
            get { return _film.Actors; }
        }

        public string MovieTitle
        {
            get { return _film.Name; }
            set
            {
                _film.Name = value;
                DisplayName = value;
                OnPropertyChanged();
            }
        }

        public int MovieLength
        {
            get { return _film.FilmLength; }
            set
            {
                _film.FilmLength = value;
                OnPropertyChanged();
            }
        }


        public int MovieBudget
        {
            get { return _film.Budget ?? 0; }
            set
            {
                _film.Budget = value;
                OnPropertyChanged();
            }
        }

        public int MovieYearReleased
        {
            get { return _film.YearReleased; }
            set
            {
                _film.YearReleased = value;
                OnPropertyChanged();
            }
        }

        public string MovieTagline
        {
            get { return _film.Tagline; }
            set
            {
                _film.Tagline = value;
                OnPropertyChanged();
            }
        }

        
       #endregion //Properties

        #region Methods

        #endregion //Methods
    }
}
