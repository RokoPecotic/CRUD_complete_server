using rpecot_lab_07.Models;

namespace rpecot_lab_07.Repositories
{
    public interface ICityRepository
    {
       public IEnumerable<City> GetCities();
        public City GetCity(long id);
       public IEnumerable<City> AddCity(City city);
       public IEnumerable<City> DeleteCity(long id);
       public IEnumerable<City> UpdateCity(long id, City city);
    }
}
