namespace FilmRent.EventArgs
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LogInSuccessArgs
        :EventArgs
    {
        #region Fields
        Account _account;
        #endregion //Fields
        #region Constructors
        public LogInSuccessArgs(Account account)
        {
            Account = account;
        }
        #endregion //Constructors

        #region Properties
        public Account Account
        {
            get { return _account; }
            private set { _account = value; }
        }
        #endregion //Properties
    }
}
