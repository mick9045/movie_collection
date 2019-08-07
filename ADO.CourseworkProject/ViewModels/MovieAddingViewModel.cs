namespace FilmRent.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Abstract;
    using Commands;
    using Service;
    using System.IO;
    using System.Windows.Media.Imaging;
    using System.Runtime.Serialization;
    using System.Collections.ObjectModel;
    using EventArgs;

    public class MovieAddingViewModel
        : WorkspaceViewModel
    {
        #region Fields
        private Film _film;
        private ICommand _loadPhotoCommand;
        private ICommand _addCountryCommand;
        private ICommand _addGenreCommand;
        private ICommand _deleteCountryCommand;
        private ICommand _deleteGenreCommand;
        private ICommand _saveCommand;
        private ICommand _selectedActorDetialsCommand;
        private ICommand _selectedMovieActorDetailsCommand;
        private ICommand _deleteSelectedMovieActorCommand;
        private ICommand _addSelectedActorCommand;
        private ICommand _searchActorCommand;
        private Country _selectedCountry;
        private Genre _selectedGenre;
        private Actor _selectedMovieActor;
        private Actor _selectedActor;
        private Country _selectedMovieCountry;
        private Genre _selectedMovieGenre;
        private ObservableCollection<Country> _movieCountries;
        private ObservableCollection<Genre> _movieGenres;
        private ObservableCollection<Actor> _movieActors;
        private ObservableCollection<Actor> _foundActors;
        private bool _newFilm;
        private bool _actorSearching;
        private string _actorSearchQuery;
        private List<string> _deleteImages = new List<string>();
        #endregion //Fields

        #region Constructors
        public MovieAddingViewModel(string displayName, Film film)
            : base(displayName)
        {
            _loadPhotoCommand = new RelayCommand(
                LoadPhotoCommandExecute,
                LoadPhotoCommandCanExecute
                );

            _addCountryCommand = new RelayCommand(
                AddCountryCommandExecute,
                AddCountryCommandCanExecute
                );

            _addGenreCommand = new RelayCommand(
                AddGenreCommandExecute,
                AddGenreCommandCanExecute
                );

            _deleteCountryCommand = new RelayCommand(
                DeleteCountryCommandExecute,
                DeleteCountryCommandCanExecute
                );

            _deleteGenreCommand = new RelayCommand(
                DeleteGenreCommandExecute,
                DeleteGenreCommandCanExecute
                );

            _saveCommand = new RelayCommand(
                SaveCommandExecute,
                SaveCommandCanExectue
                );

            _selectedMovieActorDetailsCommand = new RelayCommand(
                MovieActorDetailsCommandExecute,
                MovieActorDetailsCommandCanExecute
                );

            _deleteSelectedMovieActorCommand = new RelayCommand(
                DeleteSelectedMovieActorCommandExectue,
                DeleteSelectedMovieActorCommandCanExecute
                );

            _selectedActorDetialsCommand = new RelayCommand(
                ActorDetialsCommandExecute,
                ActorDetailsCommandCanExecute
                );

            _addSelectedActorCommand = new RelayCommand(
                AddSelectedActorCommandExecute,
                AddSelectedActorCommandCanExecute
                );

            _searchActorCommand = new RelayCommand(
                SearchActorCommandExecuteAsync,
                SearchActorCommandCanExecute
                );

            if (film == null)
            {
                _film = new Film();
                _newFilm = true;
                _movieCountries = new ObservableCollection<Country>();
                _movieGenres = new ObservableCollection<Genre>();
                _movieActors = new ObservableCollection<Actor>();
            }
            else
            {
                _film = unitOfWork.Films.GetFilmMapped(film);
                _movieCountries = new ObservableCollection<Country>(film.Countries);
                _movieGenres = new ObservableCollection<Genre>(film.Genres);
                _movieActors = new ObservableCollection<Actor>(film.Actors);
            }
            _foundActors = new ObservableCollection<Actor>();
            _actorSearching = false;
            unitOfWork.Films.RepositoryChanges += Films_RepositoryChanges;
        }

        public MovieAddingViewModel(string displayName)
            : this(displayName, null)
        {
        }

        ~MovieAddingViewModel()
        {
            OnDispose();
        }
        #endregion //Constructors

        #region Properties
        public string ActorSearchQuery
        {
            get { return _actorSearchQuery; }
            set
            {
                _actorSearchQuery = value;
                OnPropertyChanged();
            }
        }

        public Actor SelectedActor
        {
            get { return _selectedActor; }
            set
            {
                _selectedActor = value;
                OnPropertyChanged();
            }
        }

        public Actor SelectedMovieActor
        {
            get { return _selectedMovieActor; }
            set
            {
                _selectedMovieActor = value;
            }
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

        public int MovieBudget
        {
            get { return _film.Budget ?? 0; }
            set
            {
                _film.Budget = value;
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

        public Country SelectedMovieCountry
        {
            get { return _selectedMovieCountry; }
            set
            {
                _selectedMovieCountry = value;
                OnPropertyChanged();
            }
        }

        public Genre SelectedMovieGenre
        {
            get { return _selectedMovieGenre; }
            set
            {
                _selectedMovieGenre = value;
                OnPropertyChanged();
            }
        }


        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Country> AllCountries
        {
            get { return unitOfWork.Countries.GetAll(); }
        }

        public IEnumerable<Genre> AllGenres
        {
            get { return unitOfWork.Genres.GetAll(); }
        }

        public ObservableCollection<Country> MovieCountries
        {
            get { return _movieCountries; }
        }

        public ObservableCollection<Genre> MovieGenres
        {
            get { return _movieGenres; }
        }

        public ObservableCollection<Actor> MovieActors
        {
            get { return _movieActors; }
        }

        public ObservableCollection<Actor> FoundActors
        {
            get { return _foundActors; }
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

        public ICommand LoadPhotoCommand
        {
            get { return _loadPhotoCommand; }
        }

        public ICommand AddGenreCommand
        {
            get { return _addGenreCommand; }
        }

        public ICommand AddCountryCommand
        {
            get { return _addCountryCommand; }
        }

        public ICommand DeleteGenreCommand
        {
            get { return _deleteGenreCommand; }
        }

        public ICommand DeleteCountryCommand
        {
            get { return _deleteCountryCommand; }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }

        public ICommand SelectedActorDetailsCommand
        {
            get { return _selectedActorDetialsCommand; }
        }

        public ICommand SelectedMovieActorDetialsCommand
        {
            get { return _selectedMovieActorDetailsCommand; }
        }

        public ICommand DeleteSelectedMovieActorCommand
        {
            get { return _deleteSelectedMovieActorCommand; }
        }

        public ICommand AddSelectedActorCommand
        {
            get { return _addSelectedActorCommand; }
        }

        public ICommand SearchActorCommand
        {
            get { return _searchActorCommand; }
        }

        public int CurrentYear
        {
            get { return DateTime.Now.Year; }
        }
        #endregion Properties

        #region Events
        public event EventHandler<RequestActorWorkspaceArgs> RequestActorWorkspace;
        #endregion //Eventsk

        #region Methods
        protected override void OnDispose()
        {
            base.OnDispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            foreach (string path in _deleteImages)
            {
                try
                {
                    File.Delete(path);
                }
                catch
                { }
            }
        }

        private void Films_RepositoryChanges(object sender, NotifyRepositoryChangesArgs<Film> e)
        {
            if (e.Action == RepositoryAction.Remove && e.Items.FirstOrDefault() == _film)
            {
                CloseCommand.Execute(this);
            }
        }

        private async void SearchActorCommandExecuteAsync(object obj)
        {
            if (SearchActorCommandCanExecute(obj))
            {
                if (!_actorSearching && !string.IsNullOrEmpty(_actorSearchQuery))
                {
                    IEnumerable<Actor> result;
                    _actorSearching = true;
                    string[] strings = _actorSearchQuery.Split(new char[] { ' ' }, 1);
                    if (strings.Count() == 1)
                    {
                        string firstName = strings[0];
                        result = await Task.Run(() => { return unitOfWork.Actors.SearchActors(firstName); });
                        _actorSearching = false;
                    }
                    else
                    {
                        string firstName = strings[0];
                        string lastName = strings[1];
                        result = await Task.Run(() => { return unitOfWork.Actors.SearchActors(firstName, lastName); });
                        _actorSearching = false;
                    }
                    foreach (var Actor in result)
                    {
                        Actor.FirstName = Actor.FirstName.Trim();
                        Actor.LastName = Actor.LastName.Trim();
                    }
                    _foundActors = new ObservableCollection<Actor>(result);
                    OnPropertyChanged("FoundActors");
                    _actorSearching = false;
                        
                }
            }
                   
        }

        private bool SearchActorCommandCanExecute(object obj)
        {
            return true;
        }

        private void AddSelectedActorCommandExecute(object obj)
        {
            if (AddSelectedActorCommandCanExecute(obj))
            {
                _movieActors.Add(_selectedActor);
            }
        }

        private bool AddSelectedActorCommandCanExecute(object obj)
        {
            return _selectedActor != null && !_movieActors.Contains(_selectedActor);
        }

        private void DeleteSelectedMovieActorCommandExectue(object obj)
        {
            if (DeleteSelectedMovieActorCommandCanExecute(obj))
            {
                MovieActors.Remove(_selectedMovieActor);
            }
        }

        private bool DeleteSelectedMovieActorCommandCanExecute(object obj)
        {
            return _selectedMovieActor != null;
        }

        private void MovieActorDetailsCommandExecute(object obj)
        {
            if (MovieActorDetailsCommandCanExecute(obj))
            {
                RequestActorWorkspace?.Invoke(
                    this,
                    new RequestActorWorkspaceArgs(_selectedMovieActor)
                    );
            }
        }

        private bool MovieActorDetailsCommandCanExecute(object obj)
        {
            return _selectedMovieActor != null;
        }

        private void ActorDetialsCommandExecute(object obj)
        {
            if (ActorDetailsCommandCanExecute(obj))
            {
                RequestActorWorkspace?.Invoke(
                    this,
                    new RequestActorWorkspaceArgs(_selectedActor)
                    );
            }
        }

        private bool ActorDetailsCommandCanExecute(object obj)
        {
            return _selectedActor != null;
        }

        private void SaveCommandExecute(object obj)
        {
            if (SaveCommandCanExectue(obj))
            {
                _film.Countries = _movieCountries;
                _film.Genres = _movieGenres;
                _film.Actors = _movieActors;
                if (_newFilm)
                {
                    unitOfWork.Films.Add(_film);
                    _newFilm = false;
                }
                unitOfWork.SaveChanges();
                MsgBox.ShowNotification("Операция завершена успешно!");
            }
        }

        private bool SaveCommandCanExectue(object obj)
        {
            return !string.IsNullOrEmpty(_film.Name) && _film.YearReleased != 0 && _film.FilmLength != 0;
        }

        private void DeleteGenreCommandExecute(object obj)
        {
            if (DeleteGenreCommandCanExecute(obj))
            {
                _movieGenres.Remove(_selectedMovieGenre);
            }
        }

        private bool DeleteGenreCommandCanExecute(object obj)
        {
            return _selectedMovieGenre != null;
        }

        private void DeleteCountryCommandExecute(object obj)
        {
            if (DeleteCountryCommandCanExecute(obj))
            {
                _movieCountries.Remove(_selectedMovieCountry);
            }
        }

        private bool DeleteCountryCommandCanExecute(object obj)
        {
            return _selectedMovieCountry != null;
        }

        private void AddGenreCommandExecute(object obj)
        {
            if (AddGenreCommandCanExecute(obj))
            {
                _movieGenres.Add(_selectedGenre);
            }
        }

        private bool AddGenreCommandCanExecute(object obj)
        {
            return _selectedGenre != null && !_movieGenres.Contains(_selectedGenre);
        }

        private void AddCountryCommandExecute(object obj)
        {
            if (AddCountryCommandCanExecute(obj))
            {
                _movieCountries.Add(_selectedCountry);
            }
        }

        private bool AddCountryCommandCanExecute(object obj)
        {
            return _selectedCountry != null && !_movieCountries.Contains(_selectedCountry); ;
        }

        private void LoadPhotoCommandExecute(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (openFileDialog.Request())
            {
                string path = ".\\Images\\Movies\\" + Path.GetRandomFileName() +
                    Path.GetExtension(openFileDialog.FileName);
                string folderPath = ".\\Images\\Movies";
                if (!Directory.Exists(folderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    catch
                    {
                        return;
                    }
                }
                try
                {
                    if (File.Exists(_film.ImagePath))
                    {
                        _deleteImages.Add(_film.ImagePath);
                    }
                    File.Copy(openFileDialog.FileName, path, true);
                    ImagePath = path;
                }
                catch
                { }
            }

        }

        private bool LoadPhotoCommandCanExecute(object obj)
        {
            return true;
        }
        #endregion //Methods
    }
}
