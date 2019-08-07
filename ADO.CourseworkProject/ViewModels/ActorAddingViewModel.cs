namespace FilmRent.ViewModels
{
    using System;
    using System.Collections.Generic;
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
    using System.Windows.Input;

    public class ActorAddingViewModel
        : WorkspaceViewModel
    {
        #region Fields
        private Actor _actor;
        private ICommand _loadPhotoCommand;
        private ICommand _saveCommand;
        private bool _newActor;
        private List<string> _deleteImages = new List<string>();
        private Country _selectedCountry;
        #endregion //Fields
        #region Constructors
        public ActorAddingViewModel(string displayName, Actor actor)
            : base(displayName)
        {
            _loadPhotoCommand = new RelayCommand(
               LoadPhotoCommandExecute,
               LoadPhotoCommandCanExecute
               );

            _saveCommand = new RelayCommand(
               SaveCommandExecute,
               SaveCommandCanExectue
               );

            if (actor == null)
            {
                _newActor = true;
                _actor = new Actor();
                _actor.Born = new DateTime(1980, 1, 1);
            }
            else
            {
                _actor = actor;
            }
            if (_actor.Country != null)
            {
                SelectedCountry = _actor.Country;
            }
        }
        #endregion //Constructors

        #region Properties
        public IEnumerable<Country> AllCountries
        {
            get { return unitOfWork.Countries.GetAll(); }
        }

        public DateTime CurrentDate
        {
            get { return DateTime.Now; }
        }

        public ICommand LoadPhotoCommand
        {
            get { return _loadPhotoCommand; }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }

        public string ActorFirstName
        {
            get { return _actor.FirstName; }
            set
            {
                _actor.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string ActorLastName
        {
            get { return _actor.LastName; }
            set
            {
                _actor.LastName = value;
                OnPropertyChanged();
            }
        }

        public DateTime ActorBornDate
        {
            get { return _actor.Born; }
            set
            {
                _actor.Born = value;
                OnPropertyChanged();
            }
        }

        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                _actor.Country = _selectedCountry;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get { return _actor.ImagePath; }
            private set
            {
                _actor.ImagePath = value;
                OnPropertyChanged();
            }
        }
        #endregion //Properties

        #region Methods
        private void SaveCommandExecute(object obj)
        {
            if (SaveCommandCanExectue(obj))
            {
                if (_newActor)
                {
                    unitOfWork.Actors.Add(_actor);
                    _newActor = false;
                }
                unitOfWork.SaveChanges();
                MsgBox.ShowNotification("Операция завершена успешно!");
            }
        }

        private bool SaveCommandCanExectue(object obj)
        {
            return SelectedCountry != null &&
                !string.IsNullOrWhiteSpace(ActorFirstName) &&
                !string.IsNullOrWhiteSpace(ActorLastName) &&
                ActorBornDate > new DateTime(1800, 1, 1);
        }

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

        private void LoadPhotoCommandExecute(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (openFileDialog.Request())
            {
                string path = ".\\Images\\Actors\\" + Path.GetRandomFileName() +
                    Path.GetExtension(openFileDialog.FileName);
                string folderPath = ".\\Images\\Actors";
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
                    if (File.Exists(_actor.ImagePath))
                    {
                        _deleteImages.Add(_actor.ImagePath);
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
