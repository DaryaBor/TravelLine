using MovieApp.Models;
using MovieApp.Repositories;

const string connectionString = @"Data Source=USER-PC\SQLEXPRESS; Initial Catalog = Moviesdb; Integrated Security = True";

IFilmRepository filmRepository = new RawSqlFilmRepository(connectionString);
ISeanceRepository seanceRepository = new RawSqlSeanceRepository(connectionString);
ITicketsRepository ticketsRepository = new RawSqlTicketsRepository(connectionString);


while (true)
{
    Console.WriteLine("Введите команду:");
    string command = Console.ReadLine();

    if (command == "get-films")
    {
        IReadOnlyList<Film> films = filmRepository.GetAll();
        if (films.Count == 0)
        {
            Console.WriteLine("Фильмы не найдены!");
            continue;
        }

        foreach (Film film in films)
        {
            Console.WriteLine($"Id: {film.Id}, Name: {film.Denomination}, Date: {film.DateStart}, Company: {film.Company}");
        }
    }
    else if (command == "get-film-by-name")
    {
        Console.WriteLine("Введите название фильма:");
        string name = Console.ReadLine();
        Film film = (Film)filmRepository.GetByDenomination(name);
        if (name == null)
        {
            Console.WriteLine("Фильм не найден");
        }
        else
        {
            Console.WriteLine($"Id: {film.Id}, Name: {film.Denomination}, Date: {film.DateStart}, Company: {film.Company}");
        }

    }
    else if (command == "get-seance-by-id")
    {
        Console.WriteLine("Введите Id сеанса:");
        string temp = Console.ReadLine();
        int id;
        if (!int.TryParse(temp, out id))
        {
            Console.WriteLine("Неккоректный id");
            continue;
        }
        Seance Seance = seanceRepository.GetById(id);
        if (Seance == null)
        {
            Console.WriteLine("Сеансы не найдены");
            continue;
        }
        else
        {
            Console.WriteLine($"Id: {Seance.Id}, Date: {Seance.DateSeance}, Title: {Seance.Title}, FilmId: {Seance.FilmId}");
        }
    }
    else if (command == "get-seances")
    {
        IReadOnlyList<Seance> Seances = seanceRepository.GetAll();
        if (Seances.Count == 0)
        {
            Console.WriteLine("Сеансы не найдены!");
            continue;
        }
        foreach (Seance Seance in Seances)
        {
            Console.WriteLine($"Id: {Seance.Id}, Date: {Seance.DateSeance}, Title: {Seance.Title}, FilmId: {Seance.FilmId}");
        }
    }
    else if (command == "update-seance-by-id")
    {
        Console.WriteLine("Введите Id:");
        string temp = Console.ReadLine();
        int id;
        if (!int.TryParse(temp, out id))
        {
            Console.WriteLine("Неккоректный id");
            continue;
        }

        Seance Seance = seanceRepository.GetById(id);
        if (Seance == null)
        {
            Console.WriteLine("Сеанс не найден");
            continue;
        }

        Console.WriteLine("Введите новую дату:");
        int newDateSeance = Convert.ToInt32(Console.ReadLine());
        if ((newDateSeance == 0))
        {
            Console.WriteLine("Неккоректная дата:");
            continue;
        }
        Seance.UpdateDateSeance(newDateSeance);

        IReadOnlyList<Film> films = filmRepository.GetAll();
        List<int> FilmId = new List<int>();
        foreach (Film film in films)
        {
            FilmId.Add(film.Id);
        }
        Console.WriteLine("Введите новое id сеанса:");
        temp = Console.ReadLine();
        if (!int.TryParse(temp, out id) || !FilmId.Contains(id))
        {
            Console.WriteLine("Неккоректный id");
            continue;
        }
        Seance.UpdateFilmId(id);

        Console.WriteLine("Введите новое название фильма:");
        string newTitle = Console.ReadLine();
        if (string.IsNullOrEmpty(newTitle))
        {
            Console.WriteLine("Неккоректное название:");
            continue;
        }
        Seance.UpdateTitle(newTitle);

        seanceRepository.Update(Seance);
        Console.WriteLine("Сеанс обновлен");
    }

    else if (command == "delete-seance-by-id")
    {
        Console.WriteLine("Введите id сеанса:");
        int num = Convert.ToInt32(Console.ReadLine());
        Seance seance = seanceRepository.GetById(num);
        if (num == null)
        {
            Console.WriteLine("Сеанс не найден");
            continue;
        }
        else
        {
            seanceRepository.Delete(seance);
            Console.WriteLine("Сеанс удален");
        }
    }
    else if (command == "get-tickets")
    {
        IReadOnlyList<Tickets> tickets = ticketsRepository.GetAll();
        if (tickets.Count == 0)
        {
            Console.WriteLine("Билеты не найдены!");
            continue;
        }

        foreach (Tickets ticket in tickets)
        {
            Console.WriteLine($"Id: {ticket.Id}, Number: {ticket.Number}, Seance: {ticket.SeanceNumber}, Place: {ticket.Place}, Cost{ticket.Cost}, SeanceId: {ticket.SeanceId}");
        }
    }

    else if (command == "help")
    {
        PrintCommands();
    }
    else if (command == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Неправильно введенная команда");
    }
    }

    void PrintCommands()
    {
        Console.WriteLine("Доступные команды:\n");
        Console.WriteLine("get-films - Получить список фильмов");
        Console.WriteLine("get-films-by-name - Получить фильм по названию");
        Console.WriteLine("delete-seance-by-id - Удалить сеанс по id");
        Console.WriteLine("get-seances - Получить список сеансов");
        Console.WriteLine("get-seance-by-id - Получить сеанс по id");
        Console.WriteLine("update-seance-by-id - Изменить поля сеанса по id");
        Console.WriteLine("group-tickets-by-seance - Количество билетов сеанса");
        Console.WriteLine("get-tickets - Получить список билетов");

        Console.WriteLine("help - все команды");
        Console.WriteLine("exit - Выход\n");
    }
    