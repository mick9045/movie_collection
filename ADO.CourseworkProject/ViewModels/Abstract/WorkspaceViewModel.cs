namespace FilmRent.ViewModels.Abstract
{
    using System;
    using System.Windows.Input;
    using FilmRent.ViewModels.Commands;
    using DataLayer;

    public class WorkspaceViewModel
        : ViewModelBase
    {
        #region Fields
        protected readonly static UnitOfWork unitOfWork = new UnitOfWork();
        private ICommand _CloseCommand;
        #endregion //Fields

        #region Constructors
        public WorkspaceViewModel(string displayName)
            : base(displayName)
        {
            CloseCommand = new Commands.RelayCommand(
                CloseCommandExecute,
                CloseCommandCanExectue
                );
        }
        #endregion //Constructors

        #region Events
        public event EventHandler RequestClose;
        #endregion //Events

        #region Properties
        public ICommand CloseCommand
        {
            get
            {
                return _CloseCommand;
            }
            private set
            {
                _CloseCommand = value;
                OnPropertyChanged();
            }
        }
        #endregion //Properties

        #region Methods
        protected virtual bool CloseCommandCanExectue(object obj)
        {
            return true;
        }

        protected void CloseCommandExecute(object obj)
        {
            RequestClose?.Invoke(this, new EventArgs());
        }
        #endregion //Methods
    }
}
