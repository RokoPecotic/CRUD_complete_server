using Microsoft.Extensions.Options;
using rpecot_lab_07.Configuration;
using rpecot_lab_07.Exceptions;
using rpecot_lab_07.Models;
using rpecot_lab_07.Repositories;
using System.Text.RegularExpressions;

namespace rpecot_lab_07.Logic
{
    public class CityLogic : ICityLogic
    {
        private readonly ICityRepository _cityRepository;
        private readonly ValidationConfiguration _validationConfiguration;

        public CityLogic(ICityRepository cityRepository, IOptions<ValidationConfiguration> validationConfiguration)
        {
            _cityRepository = cityRepository;
            _validationConfiguration = validationConfiguration.Value;
        }

        private bool IsCityNameValid(string cityName)
        {
            return !string.IsNullOrWhiteSpace(cityName) &&
                   cityName.Length <= _validationConfiguration.MaxCityNameLength &&
                   Regex.IsMatch(cityName, @"^[a-zA-Z\s]+$");
        }

        private bool IsMayorValid(string mayor)
        {
            return !string.IsNullOrWhiteSpace(mayor) &&
                   mayor.Length <= _validationConfiguration.MaxMayorLength &&
                   Regex.IsMatch(mayor, @"^[a-zA-Z\s]+$");
        }

        private bool IsResidentsValid(List<string> residents)
        {
            return residents != null &&
                   residents.Count >= 3 &&
                   residents.All(resident => resident.Length <= _validationConfiguration.MaxResidentsLength);
        }

        private bool IsClimateValid(string climate)
        {
            var validClimates = new List<string> { "Sunny", "Rainy", "Cloudy", "Windy", "Hot", "Cold", "Warm" };
            return climate.Length <= _validationConfiguration.MaxClimateLength && validClimates.Contains(climate);
        }

        private void ValidateMayor(City city)
        {
            if (string.IsNullOrWhiteSpace(city.Mayor))
            {
                throw new CityAppException_UserError("A city must have a mayor.");
            }
        }

        private void ValidateCity(City city)
        {
            if (!IsCityNameValid(city.CityName))
            {
                throw new CityAppException_UserError("City name must only contain letters and spaces and be within the allowed length.");
            }
            else if (!IsMayorValid(city.Mayor))
            {
                throw new CityAppException_UserError("Mayor must only contain letters and spaces and be within the allowed length.");
            }
            else if (!IsResidentsValid(city.Residents))
            {
                throw new CityAppException_UserError($"City '{city.CityName}' must have at least 3 residents, and each resident's name must not exceed the maximum length.");
            }
            else if (!IsClimateValid(city.Climate))
            {
                throw new CityAppException_UserError($"Climate in '{city.CityName}' can be one of: Sunny, Rainy, Cloudy, Windy, Hot, Cold, Warm, and must not exceed the maximum length.");
            }

            ValidateMayor(city);
        }

        public IEnumerable<City> GetCities()
        {
            return _cityRepository.GetCities();
        }
        public City GetCity(long id)
        {
            return _cityRepository.GetCity(id);
        }
        public IEnumerable<City> AddCity(City city)
        {
            ValidateCity(city);
            _cityRepository.AddCity(city);
            return _cityRepository.GetCities();
        }

        public IEnumerable<City> DeleteCity(long id)
        {
            var cityToDelete = _cityRepository.GetCities().FirstOrDefault(c => c.Id == id);

            if (cityToDelete == null)
            {
                throw new CityAppException_UserError("City not found.");
            }

            _cityRepository.DeleteCity(id);
            return _cityRepository.GetCities();
        }

        public IEnumerable<City> UpdateCity(long id, City updatedCity)
        {
            var cityToUpdate = _cityRepository.GetCities().FirstOrDefault(c => c.Id == id);

            if (cityToUpdate == null)
            {
                throw new CityAppException_UserError("City not found.");
            }

            ValidateCity(updatedCity);
            _cityRepository.UpdateCity(id, updatedCity);
            return _cityRepository.GetCities();
        }
    }
}
