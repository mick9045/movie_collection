namespace FilmRent.ViewModels
{
    using Abstract;
    using Commands;
    using Service;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Models;
    using System.Threading.Tasks;
    using EventArgs;
    using System.IO;

    public class MovieSearchViewModel
        : WorkspaceViewModel
    {
        #region Fields

        #endregion //Fields
        private ICommand _detailedViewCommand;
        private ICommand _removeCommand;
        private ICommand _editCommand;
        private ICommand _searchCommand;
        private string _searchQuery;
        private ObservableCollection<Film> _resultCollection;
        private Film _selectedMovie;
        private bool _searching;
        #region Constructors
        public MovieSearchViewModel(string displayName) : base(displayName)
        {
            _detailedViewCommand = new RelayCommand(
                DetailedViewCommandExecute,
                DetailedViewCommandCanExecute
                );

            _removeCommand = new RelayCommand(
                RemoveCommandEcecute,
                RemoveCommandCanExecute
                );

            _editCommand = new RelayCommand(
                EditCommandExectue,
                EditCommandCanExecute
                );

            _searchCommand = new RelayCommand(
                SearchCommandExecuteAsync,
                SearchCommandCanExecute
                );

            _resultCollection = new ObservableCollection<Film>();
            _searching = false;
        }
        #endregion //Constructors

        #region Properties
        public Film SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Film> ResultCollection
        {
            get { return _resultCollection; }
            private set
            {
                _resultCollection = value;
                OnPropertyChanged();
            }
        }


        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }

        public ICommand DetailedViewCommand
        {
            get { return _detailedViewCommand; }
        }

        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
        }

        public ICommand EditCommand
        {
            get { return _editCommand; }
        }
        #endregion //Properties

        #region Events
        public event EventHandler<MovieRequestWorkspaceArgs> MovieWorkspaceRequest;
        #endregion //Events;

        #region Methods
        private async void SearchCommandExecuteAsync(object obj)
        {
            if (!_searching)
            {
                if (!string.IsNullOrWhiteSpace(_searchQuery))
                {
                    _searching = true;
                    await Task.Run(() => ResultCollection = new ObservableCollection<Film>(
                        unitOfWork.Films.SearchFilmsByTitle(_searchQuery)
                        ));
                    _searching = false;
                }
            }
        }

        private bool SearchCommandCanExecute(object obj)
        {
            return !_searching;
        }

        private void DetailedViewCommandExecute(object obj)
        {
            if (DetailedViewCommandCanExecute(obj))
            {
                MovieWorkspaceRequest?.Invoke(
                    this,
                    new MovieRequestWorkspaceArgs(MovieAction.View, _selectedMovie)
                    );
            }
        }

        private bool DetailedViewCommandCanExecute(object obj)
        {
            return _selectedMovie != null;
        }

        private void RemoveCommandEcecute(object obj)
        {
            if (MsgBox.AskForConfirmation("Вы собираетесь удалить фильм! Продолжить?"))
            {
                try
                {
                    File.Delete(_selectedMovie.ImagePath);
                }
                catch { }
                unitOfWork.Films.Remove(
                    _selectedMovie
                    );
                _resultCollection.Remove(_selectedMovie);
                unitOfWork.SaveChanges();

            }
        }

        private bool RemoveCommandCanExecute(object obj)
        {
            return _selectedMovie != null;
        }

        private void EditCommandExectue(object obj)
        {
            if (EditCommandCanExecute(obj))
            {
                MovieWorkspaceRequest?.Invoke(
                    this,
                    new MovieRequestWorkspaceArgs(MovieAction.Edit, _selectedMovie)
                    );
            }
        }

        private bool EditCommandCanExecute(object obj)
        {
            return _selectedMovie != null;
        }

        #endregion //Methods
    }

}
