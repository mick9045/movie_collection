using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FilmRent.ViewModels.Abstract
{
    public abstract class ViewModelBase
        : INotifyPropertyChanged, IDisposable
    {
        #region Fields
        private string _displayName;
        #endregion //Fields

        #region Constructors
        public ViewModelBase(string displayName)
        {
            DisplayName = displayName;
        }
        #endregion //Constructors

        #region Properties
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged();
            }
        }
        #endregion // Properties

        #region IDisposable members

        public void Dispose()
        {
            OnDispose();
        }
        #endregion // Idposable members

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion //INotifyPropertyChanged members

        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnDispose()
        {
        }
        #endregion //Methods
    }
}
