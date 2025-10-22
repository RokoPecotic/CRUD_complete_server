using System.Text.Json.Serialization;
using rpecot_lab_07.Models;

namespace rpecot_lab_07.DTO
{
    public class CityDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("residents")]
        public string? Residents { get; set; }
        [JsonPropertyName("cityname")]
        public string? CityName { get; set; }
        [JsonPropertyName("mayor")]
        public string? Mayor { get; set; }
        [JsonPropertyName("climate")]
        public string? Climate { get; set; }
        [JsonPropertyName("datenow")]
        public DateTime DateNow { get; set; }


        public City ToModel()
        {
            return new City(
                City.ResidentsFromString(this.Residents),
                this.CityName,
                this.Mayor,
                this.Climate,
                this.DateNow,
                this.Id
            );
        }

        public static CityDTO FromModel(City model)
        {
            return new CityDTO
            {
                Id = model.Id,
                Residents = model.ResidentsAsString(),
                CityName = model.CityName,
                Mayor = model.Mayor,
                Climate = model.Climate,
                DateNow = model.DateNow
            };
        }

    }
}
