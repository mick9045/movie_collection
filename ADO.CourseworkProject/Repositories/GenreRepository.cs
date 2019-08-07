namespace FilmRent.Repositories
{
    using Abstract;
    using Models;

    public class GenreRepository
        : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(FilmRentContext context) 
            : base(context)
        {
        }
    }
}
