using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmRent.DataLayer.Exceptions
{
    class NoPermissionException
        : Exception
    {
        public NoPermissionException()
            : base("Access denied")
        {
        }
        
        public NoPermissionException(string message)
            : base(message)
        {
        }

        public NoPermissionException(string message, Exception e)
            :base(message, e)
        {
        }
    }
}
