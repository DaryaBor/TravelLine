using MovieApp.Models;
namespace MovieApp.Repositories
{
    public interface ISeanceRepository
    {
        IReadOnlyList<Seance> GetAll();
        Seance GetById(int id);
        void Update(Seance seance);
        void Delete(Seance seance);
    }
}