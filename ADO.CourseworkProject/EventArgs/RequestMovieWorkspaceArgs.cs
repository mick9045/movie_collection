namespace FilmRent.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    public enum MovieAction
    {
        Edit, View
    }

    public class MovieRequestWorkspaceArgs
        : EventArgs
    {
        #region Fields
        MovieAction _action;
        Film _film;
        #endregion //Fields
        #region Constructors
        public MovieRequestWorkspaceArgs(MovieAction action, Film film)
        {
            _action = action;
            _film = film;
        }
        #endregion //Constructors

        #region Properties
        public MovieAction Action
        {
            get { return _action; }
            private set { _action = value; }
        }

        public Film Film
        {
            get { return _film; }
            private set { _film = value; }
        }
        #endregion //Properties
    }
}
