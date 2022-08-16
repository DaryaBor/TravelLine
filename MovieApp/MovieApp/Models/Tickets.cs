namespace MovieApp.Models
{

	public class Tickets
	{
		public int Id { get; private set; }
		public int Number { get; private set; }
		public int SeanceNumber { get; private set; }
		public int Place { get; private set; }
		public int Cost { get; private set; }
		public int SeanceId { get; private set; }

		public Tickets(int Id, int Number, int SeanceNumber, int Place, int Cost, int SeanceId)
		{
			Id = id;
			Number = number;
			SeanceNumber = seanceNumber;
			Place = place;
			Cost = cost;
			SeanceId = seanceId;

		}
	}

}