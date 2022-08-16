namespace MovieApp.Models
{ 
	public class Film
	{
		public int Id { get; private set; }
		public string Denomination { get; private set; }
		public int DateStart { get; private set; }
		public string Company { get; private set; }

		public Film (int Id, string Denomination, int DateStart, string Company)
		{
			Id = id;
			Denomination = denomination;
			DateStart = date;
			Company = company;
		}
	}
}
