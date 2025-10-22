namespace rpecot_lab_07.Models
{
    public class City
    {
        public City()
        {
            Id = 0;
            DateNow = DateTime.Now;
        }
        public City(List<string> residents, string cityname, string mayor, string climate)
        {
            Id = 0;
            Residents = residents ?? new List<string>();
            CityName = cityname;
            Mayor = mayor;
            Climate = climate;
            DateNow = DateTime.Now;
        }

        public City(List<string> residents, string cityname, string mayor, string climate, DateTime datenow, long id)
        {
            Id = id;
            Residents = residents ?? new List<string>();
            CityName = cityname;
            Mayor = mayor;
            Climate = climate;
            DateNow = datenow;
        }
        

        public long Id { get; set; }
        public List<string> Residents { get; set; }
        public string CityName { get; set; }
        public string Mayor { get; set; }
        public string Climate { get; set; }
        public DateTime DateNow { get; set; }

        public string ResidentsAsString() => string.Join(",", Residents);

        public static List<string> ResidentsFromString(string residents) =>
            residents?.Split(',').ToList() ?? new List<string>();
    }
}
