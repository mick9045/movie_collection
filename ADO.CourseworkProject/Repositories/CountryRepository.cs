namespace FilmRent.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Abstract;

    public class CountryRepository
        : Repository<Country>, ICountryRepository
    {
        public CountryRepository(FilmRentContext context) : base(context)
        {
        }
    }
}
