using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmRent.Service.Abstract
{
    interface IOpenFileService
    {
        string Filter { get; set; }
        string InitialDirectory { get; set; }
        string FileName { get; }
        bool Request();
        

    }
}
