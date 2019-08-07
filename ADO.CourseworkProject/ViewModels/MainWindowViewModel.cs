namespace FilmRent.ViewModels
{
    using System;
    using Commands;
    using Models;
    using Abstract;
    using EventArgs;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using ViewModels;
    using System.Windows.Input;
    using Service;

    class MainWindowViewModel
        : WorkspaceViewModel
    {
        #region Fields
        private ObservableCollection<CommandViewModel> _mainCommands = new ObservableCollection<CommandViewModel>();
        private ObservableCollection<WorkspaceViewModel> _workspaces;
        private ICommand _requestCloseCommand;
        private WorkspaceViewModel _currentWorkpace;
        private int _currentWorkspaceIndex = -1;
        private Account _account;
        #endregion //Fields

        #region Constructors
        public MainWindowViewModel()
            : base("Коллекция Фильмов")
        {
            _mainCommands.Add(new CommandViewModel(
                "Авторизация",
                new RelayCommand(UserLogInCommandExecute, UserLogInCommandCanExecute)
                ));

            _requestCloseCommand = new RelayCommand(
                RequestCloseCommandExecute,
                RequestCloseCommandCanExecute
                );
        }

        #endregion //Constructors

        #region Events

        #endregion //Events

        #region Properties
        public ObservableCollection<CommandViewModel> MainCommands
        {
            get { return _mainCommands; }
            private set { _mainCommands = value; }
        }

        public WorkspaceViewModel CurrentWorkspace
        {
            get
            {
                return _currentWorkpace;
            }
            set
            {
                _currentWorkpace = value;
                OnPropertyChanged();
            }
        }

        public int CurrentWorkspaceIndex
        {
            get
            {
                return _currentWorkspaceIndex;
            }
            set
            {
                _currentWorkspaceIndex = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        public ICommand RequestCloseCommand
        {
            get { return _requestCloseCommand; }
            set
            {
                _requestCloseCommand = value;
                OnPropertyChanged();
            }
        }
            

        #endregion //Properties

        #region Methods
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += OnWorkspaceRequestClose;
                }
            }
            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= OnWorkspaceRequestClose;
                }
            }

            CurrentWorkspace = (_workspaces.Count > 0 ? _workspaces[_workspaces.Count - 1] : null);
            CurrentWorkspaceIndex = (_workspaces.Count > 0 ? _workspaces.Count - 1 : -1);
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = (sender as WorkspaceViewModel);
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        private void OnLoginSuccess(object sender, LogInSuccessArgs args)
        {
            _account = args.Account;
            _mainCommands.RemoveAt(0);

            _mainCommands.Add(
                new CommandViewModel(
                    "Добавить фильм",
                    new RelayCommand(MovieAddingCommandExecute, MovieAddingCommandCanExecute)
                    )
                );

            _mainCommands.Add(
                new CommandViewModel(
                "Найти фильм",
                new RelayCommand(SearchFilmCommandExectue, SearchFilmCommandCanExecute)
                    )
                );

            _mainCommands.Add(
                new CommandViewModel(
                "Добавить aктёра",
                new RelayCommand(AddActorCommandExecute, AddActorCommandCanExectue)
                    )
                );
        }

        private void AddActorCommandExecute(object obj)
        {
            var workspace = new ActorAddingViewModel("Aктёр", null);
            _workspaces.Add(workspace);
        }

        private bool AddActorCommandCanExectue(object obj)
        {
            return true;
        }

        private void RequestCloseCommandExecute(object obj)
        {
            if (RequestCloseCommandCanExecute(obj))
            {
                if (MsgBox.AskForConfirmation("Вы собираетесь закрыть приложение! Продолжить?"))
                {
                    CloseCommand.Execute(obj);
                }

            }
        }
        
        private bool RequestCloseCommandCanExecute(object obj)
        {
            return CloseCommand.CanExecute(obj);
        }

        private void SearchFilmCommandExectue(object obj)
        {
            var workspace = new MovieSearchViewModel("Поиск Фильма");
            workspace.MovieWorkspaceRequest += Workspace_MovieWorkspaceRequest;
            _workspaces.Add(workspace);
        }

        private void Workspace_MovieWorkspaceRequest(object sender, MovieRequestWorkspaceArgs e)
        {
            if (e.Action == MovieAction.Edit)
            {
                var workspace = new MovieAddingViewModel(e.Film.Name.Trim(), e.Film);
                workspace.RequestActorWorkspace += Workspace_RequestActorWorkspace;
                _workspaces.Add(workspace);
                _currentWorkpace = workspace;
            }
            else
            {
                var workspace = new MovieViewModel(e.Film.Name.Trim(), e.Film);
                _workspaces.Add(workspace);
                _currentWorkpace = workspace;
            }
        }

        private bool SearchFilmCommandCanExecute(object obj)
        {
            return true;
        }
        
        private bool MovieAddingCommandCanExecute(object obj)
        {
            return true;
        }

        private void MovieAddingCommandExecute(object obj)
        {
            var workspace = new MovieAddingViewModel("Фильм");
            workspace.RequestActorWorkspace += Workspace_RequestActorWorkspace;
            _workspaces.Add(workspace);
        }

        private void Workspace_RequestActorWorkspace(object sender, RequestActorWorkspaceArgs e)
        {
            _workspaces.Add(new ActorAddingViewModel("Актёр", e.Actor));
        }

        private bool UserLogInCommandCanExecute(object obj)
        {
            return true;
        }

        private void UserLogInCommandExecute(object obj)
        {
            UserLogInViewModel userLoginViewModel = _workspaces.Select(x => x)
                .Where(x => x is WorkspaceViewModel)
                .FirstOrDefault() as UserLogInViewModel;

            if (userLoginViewModel == null)
            {
                userLoginViewModel = new UserLogInViewModel("Авторизация");
                userLoginViewModel.LoginSuccess += OnLoginSuccess;
                _workspaces.Add(userLoginViewModel);
            }
            else
            {
                CurrentWorkspace = userLoginViewModel;
            }

        }
        #endregion //Methods
    }
}
