using rpecot_lab_07.Models;
using System.Text.Json.Serialization;

namespace rpecot_lab_07.DTO
{
    public class NewCityRequestDTO
    {
        [JsonPropertyName("residents")]
        public string? Residents { get; set; }

        [JsonPropertyName("cityname")]
        public string? CityName { get; set; }

        [JsonPropertyName("mayor")]
        public string? Mayor { get; set; }

        [JsonPropertyName("climate")]
        public string? Climate { get; set; }

        public City ToModel()
        {
            return new City(
                City.ResidentsFromString(this.Residents),
                this.CityName ?? string.Empty,
                this.Mayor ?? string.Empty,
                this.Climate ?? string.Empty
            );
        }

        public static NewCityRequestDTO FromModel(City model)
        {
            return new NewCityRequestDTO
            {
                Residents = model.ResidentsAsString(),
                CityName = model.CityName,
                Mayor = model.Mayor,
                Climate = model.Climate
            };
        }
    }
}
