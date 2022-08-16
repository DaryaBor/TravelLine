using MovieApp.Models;
namespace MovieApp.Repositories
{
    public interface IFilmRepository
    {
        IReadOnlyList<Film> GetAll();
        IReadOnlyList<Film> GetByDenomination(string denomination);
    }
}