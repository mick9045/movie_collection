namespace FilmRent.ViewModels.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    class RelayCommand
        : ICommand
    {
        #region Fields
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion //Fields

        #region Constructors
        public RelayCommand(Action<object> exectute, Predicate<object> canExecute)
        {
            _execute = exectute;
            _canExecute = canExecute;
        }
        public RelayCommand(Action<object> execute) :this(execute, null) { }
        #endregion //Constructors

        #region ICommand Memebers
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion //Icommand memebers
    }
}
