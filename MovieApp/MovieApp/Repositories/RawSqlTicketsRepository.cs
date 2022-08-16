using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using MovieApp.Models;

namespace MovieApp.Repositories
{
    public class RawSqlTicketsRepository : ITicketsRepository
    {
        private readonly string _connectionString;

        public RawSqlTicketsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Tickets> GetAll()
        {
            var result = new List<Tickets>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();


            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Number],[SeanceNumber],[Place],[Cost],[SeanceId] from [Tickets]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Tickets(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["Number"]),
                    Convert.ToInt32(reader["SeanceNumber"]),
                    Convert.ToInt32(reader["Place"]),
                    Convert.ToInt32(reader["Cost"]),
                    Convert.ToInt32(reader["SeanceId"])
                ));
            }
            return result;
        }

        public IReadOnlyList<string> GroupBySeance()
        {
            var result = new List<string>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id],[Number],[SeanceNumber],[Place],[Cost],[SeanceId],SUM([Cost]) AS [TotalCost] from [Tickets] GROUP BY [SeanceNumber]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Tickets tickets = new Tickets(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["Number"]),
                    Convert.ToInt32(reader["SeanceNumber"]),
                    Convert.ToInt32(reader["Place"]),
                    Convert.ToInt32(reader["Cost"]),
                    Convert.ToInt32(reader["SeanceId"])
                    );
                result.Add($"{Convert.ToInt32(reader["TotalCost"])}");
            }
            return result;
        }
    }
}

