using rpecot_lab_07.Models;

namespace rpecot_lab_07.Repositories
{
    public class CityRepository : ICityRepository
    {
        private List<City> City { get; set; }

        public IEnumerable<City> GetCities()
        {
            return City;
        }
        public City GetCity(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> AddCity(City city)
        {
            City.Add(city);
            return City;
        }

        public IEnumerable<City> DeleteCity(long id)
        {
            City = City.Where(x => x.Id != id).ToList();
            return City;
        }

        public IEnumerable<City> UpdateCity(long id, City updatedCity)
        {
            var city = City.FirstOrDefault(c => c.Id == id);
            if (city != null)
            {
                city.Residents = updatedCity.Residents;
                city.CityName = updatedCity.CityName;
                city.Mayor = updatedCity.Mayor;
                city.Climate = updatedCity.Climate;
                city.DateNow = updatedCity.DateNow;
            }
            return City;
        }

        public CityRepository()
        {
            City = new List<City>
            {
                new City(
                    new List<string> { "Marko", "Ante", "Stipe" },
                    "Split",
                    "Toni",
                    "Sunny",
                    DateTime.Now,
                    1
                )
            };
        }
    }
}
