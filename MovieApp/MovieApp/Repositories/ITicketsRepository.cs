using MovieApp.Models;
namespace MovieApp.Repositories
{
    public interface ITicketsRepository
    {
        IReadOnlyList<Tickets> GetAll();
        IReadOnlyList<string> GroupBySeance();
    }
}
