using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilmRent.ViewModels
{
    class CommandViewModel
        : Abstract.ViewModelBase
    {
        #region Fields
        private ICommand _command;
        #endregion //Fields

        #region Constructors
        public CommandViewModel(string displayName, ICommand command)
            : base(displayName)
        {
            Command = command;
        }
        #endregion //Constructors

        #region Properties
        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }
        #endregion //Properties
    }
}
