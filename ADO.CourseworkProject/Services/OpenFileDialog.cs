namespace FilmRent.Service
{
    using System;
    using System.IO;
    using System.Windows;
    using Microsoft.Win32;
    using Abstract;

    class OpenFileDialog
        : IOpenFileService
    {
        #region Fields
        Microsoft.Win32.OpenFileDialog _openFileDialog;
        #endregion //Fields

        #region Constructors
        public OpenFileDialog()
        {
            _openFileDialog = new Microsoft.Win32.OpenFileDialog();
        }
        #endregion //Constructors

        #region IOpenFileDialogService members
        public string Filter
        {
            get { return _openFileDialog.Filter; }

            set { _openFileDialog.Filter = value; }
        }

        public string InitialDirectory
        {
            get { return _openFileDialog.InitialDirectory; }

            set { _openFileDialog.InitialDirectory = value; }
        }


        public string FileName
        {
            get { return _openFileDialog.FileName; }
        }

        public bool Request()
        {
            return _openFileDialog.ShowDialog() ?? false;
        }
        #endregion //IOpenFileDialogService members


    }
}
