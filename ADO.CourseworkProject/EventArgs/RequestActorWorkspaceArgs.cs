namespace FilmRent.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    
    public class RequestActorWorkspaceArgs
        : EventArgs
    {
        #region //Fields
        Actor _actor;
        #endregion

        #region Constructor
        public RequestActorWorkspaceArgs(Actor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException("Actor");
            }
            _actor = actor;
        }
        #endregion //Constructor

        #region Properties
        public Actor Actor
        {
            get { return _actor; }
        }
        #endregion //Properties
    }
}
