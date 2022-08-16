namespace MovieApp.Models
{

	public class Seance
	{
		public int Id { get; private set; }
		public int DateSeance { get; private set; }
		public string Title { get; private set; }
		public int FilmId { get; private set; }
		
		public Seance(int Id, int DateSeance, string Title, int FilmId)
        {
			Id = id;
			DateSeance = dateSeance;
			Title = title;
			FilmId = filmId;
		}
		public void UpdateDateSeance(int newDateSeance)
        {
			DateSeance = newDateSeance;
        }
		public void UpdateTitle(string newTitle)
        {
			Title = newTitle;
        }
		public void UpdateFilmId(int newId)
        {
			FilmId = newId;
        }
	}
}
