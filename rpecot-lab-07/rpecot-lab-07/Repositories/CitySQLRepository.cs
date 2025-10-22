using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using rpecot_lab_07.Configuration;
using rpecot_lab_07.Models;

namespace rpecot_lab_07.Repositories
{
    public class CitySQLRepository : ICityRepository
    {

        private readonly string _connectionString;

        private readonly DBConfiguration _dbConfiguration;

        public CitySQLRepository (IOptions<DBConfiguration> dbConfiguration)
        {
            _connectionString = dbConfiguration.Value.ConnectionString;
        }

        public IEnumerable<City> AddCity(City city)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO City (Residents, CityName, Mayor, Climate, DateNow)
                VALUES ($residents, $cityname, $mayor, $climate, $date)";

            command.Parameters.AddWithValue("$residents", city.ResidentsAsString());
            command.Parameters.AddWithValue("$cityname", city.CityName);
            command.Parameters.AddWithValue("$mayor", city.Mayor);
            command.Parameters.AddWithValue("$climate", city.Climate);
            command.Parameters.AddWithValue("$date", city.DateNow);

            _ = command.ExecuteNonQuery();

            return GetCities();
        }

        public IEnumerable<City> DeleteCity(long id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM City WHERE ID = $id";
            command.Parameters.AddWithValue("$id", id);

            _ = command.ExecuteNonQuery();
            return GetCities();
        }

        public IEnumerable<City> GetCities()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT ID, Residents, CityName, Mayor, Climate, DateNow FROM City";

            using var reader = command.ExecuteReader();

            var results = new List<City>();
            while (reader.Read())
            {
                var city = new City(
                    id: reader.GetInt64(0),
                    residents: City.ResidentsFromString(reader.GetString(1)),
                    cityname: reader.GetString(2),
                    mayor: reader.GetString(3),
                    climate: reader.GetString(4),
                    datenow: reader.GetDateTime(5)
                );
                results.Add(city);
            }

            return results;
        }
        public City GetCity(long id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"SELECT ID, Residents, CityName, Mayor, Climate, DateNow FROM City WHERE Id = $id";
            command.Parameters.AddWithValue("id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new City(
                    id: reader.GetInt64(0),
                     residents: City.ResidentsFromString(reader.GetString(1)),
                    cityname: reader.GetString(2),
                    mayor: reader.GetString(3),
                    climate: reader.GetString(4),
                    datenow: reader.GetDateTime(5)
                );
            }
            return null;
        }

        public IEnumerable<City> UpdateCity(long id, City city)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                UPDATE City
                SET Residents = $residents,
                    CityName = $cityname,
                    Mayor = $mayor,
                    Climate = $climate,
                    DateNow = $date
                WHERE ID = $id";

            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$residents", city.ResidentsAsString());
            command.Parameters.AddWithValue("$cityname", city.CityName);
            command.Parameters.AddWithValue("$mayor", city.Mayor);
            command.Parameters.AddWithValue("$climate", city.Climate);
            command.Parameters.AddWithValue("$date", city.DateNow);

            _ = command.ExecuteNonQuery();
            return GetCities();
        }
    }
}
