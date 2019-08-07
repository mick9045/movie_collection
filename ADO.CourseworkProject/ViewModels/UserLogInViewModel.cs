namespace FilmRent.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FilmRent.ViewModels.Abstract;
    using System.ComponentModel;
    using EventArgs;
    using System.Collections;
    using System.Security;
    using System.Windows.Input;
    using Commands;
    using System.Windows.Controls;
    using Models;

    public class UserLogInViewModel
        : WorkspaceViewModel
    {
        #region Fields
        private string _nickname;
        private ICommand _logInCommand;
        private string _passwordError;
        private bool _logging;
        #endregion //Fields

        #region Constructors
        public UserLogInViewModel(string displayName)
            : base(displayName)
        {
            LogInCommand = new RelayCommand(LoginCommandExecuteAsync, LoginCommandCanExecute);
            Nickname = "";
            PasswordError = "";
            _logging = false;
        }
        #endregion //Constructors

        #region Events
        public event EventHandler<LogInSuccessArgs> LoginSuccess;
        #endregion

        #region Propeties
        public string Nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value;
                OnPropertyChanged();
            }
        }

        public string PasswordError
        {
            get { return _passwordError; }
            private set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogInCommand
        {
            get { return _logInCommand; }
            private set { _logInCommand = value; }
        }
        #endregion //Properties

        #region Methods
        private bool LoginCommandCanExecute(object obj)
        {
            return Nickname.Length > 0 && !_logging;
        }

        private async void LoginCommandExecuteAsync(object obj)
        {
            if (_logging == true)
            {
                return;
            }
            var passBox = obj as PasswordBox;
            Account account = null;
            
            await Task.Factory.StartNew(() =>
            {
                _logging = true;
                account = unitOfWork.Accounts.GetAccountByNickName(Nickname);
            });

            if (account == null)
            {
                Service.MsgBox.ShowWarning("Аккаунт не существует");
            }
            else if (account.Nickname.Trim() == Nickname)
            {
                if (account.Password.Trim() != passBox.Password)
                {
                    PasswordError = "Неверный пароль";
                }
                else
                {
                    LoginSuccess?.Invoke(this, new LogInSuccessArgs(account));
                    CloseCommand.Execute(null);
                }
            }
            else
            {
                PasswordError = "";
            }
            _logging = false;
        }
        #endregion //Methods
    }

}
