using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using MovieApp.Models;

namespace MovieApp.Repositories
{
    public class RawSqlFilmRepository : IFilmRepository
    {
        private readonly string _connectionString;

        public RawSqlFilmRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Film> GetAll()
        {
            var result = new List<Film>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Denomination], [DateStart],[Company] from [Film]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Film(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Denomination"]),
                    Convert.ToInt32(reader["DateStart"]),
                    Convert.ToString(reader["Company"])
                ));
            }
            return result;
        }

        public IReadOnlyList<Film> GetByDenomination(string denomination)
        {
            var result = new List<Film>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Denomination], [DateStart],[Company] from [Film] where [Denomination] = @denomination";
            sqlCommand.Parameters.Add("@denomination", SqlDbType.Int).Value = denomination;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Film(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Denomination"]),
                    Convert.ToInt32(reader["DateStart"]),
                    Convert.ToString(reader["Company"])
                ));
            }
            return result;
        }
}