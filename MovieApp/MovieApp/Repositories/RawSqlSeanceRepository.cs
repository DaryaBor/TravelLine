using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using MovieApp.Models;

namespace MovieApp.Repositories
{
    public class RawSqlSeanceRepository : ISeanceRepository
    {
        private readonly string _connectionString;

        public RawSqlSeanceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Seance> GetAll()
        {
            var result = new List<Seance>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [DateSeance], [Title],[FilmId] from [Seance]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Seance(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["DateSeance"]),
                    Convert.ToString(reader["Title"]),
                    Convert.ToInt32(reader["FilmId"])
                ));
            }
            return result;
        }
        public Seance GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [DateSeance], [Title],[FilmId] from [Seance] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Seance(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["DateSeance"]),
                    Convert.ToString(reader["Title"]),
                    Convert.ToInt32(reader["FilmId"])
                );
            }
            else
            {
                return null;
            }
        }
        public void Update(Seance seance)
        {
            if (seance == null)
            {
                throw new ArgumentNullException("null received");
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Seance] set [DateSeance] = @dateSeance, [Title] = @title, [FilmId] = @filmId where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = seance.Id;
            sqlCommand.Parameters.Add("@dateSeance", SqlDbType.Int).Value = seance.DateSeance;
            sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = seance.Title;
            sqlCommand.Parameters.Add("@filmId", SqlDbType.Int).Value = seance.FilmId;
            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(Seance seance)
        {
            if (seance == null)
            {
                throw new ArgumentNullException("null received");
            }
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Seance] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = seance.Id;
            sqlCommand.ExecuteNonQuery();
        }
    }
}