using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rpecot_lab_07.Models;
using rpecot_lab_07.Repositories;
using rpecot_lab_07.Logic;
using rpecot_lab_07.Filters;
using rpecot_lab_07.DTO;

namespace rpecot_lab_07.Controllers
{
    [Route("api/[controller]")]
    [ErrorFilter]
    [ApiController]
    public class CityController : ControllerBase
    {

        private ICityLogic _cityLogic;

        public CityController(ICityLogic cityRepository)
        {
            _cityLogic = cityRepository;
        }

        [HttpGet("all")]
        public IEnumerable<CityDTO> GetCities()
        {
            var cityList = _cityLogic.GetCities();
            return cityList.Select(x => CityDTO.FromModel(x));
        }
        [HttpGet("one/{id}")]
        public CityDTO GetCity([FromRoute] long id)
        {
            var city = _cityLogic.GetCity(id);
            if (city == null)
            {
                return null;
            }
            return CityDTO.FromModel(city);
        }
        [HttpPost("new")]
        public IEnumerable<CityDTO> AddNewCity([FromBody] NewCityRequestDTO city)
        {
            City model = city.ToModel();

            var cityList = _cityLogic.AddCity(model);
            return cityList.Select(x => CityDTO.FromModel(x));
        }
        [HttpDelete("delete/{id}")]
        public IEnumerable<CityDTO> DeleteCity([FromRoute] long id)
        {
            var emailList = _cityLogic.DeleteCity(id);
            return emailList.Select(x => CityDTO.FromModel(x));

        }
        [HttpPost("edit/{id}")]
        public IEnumerable<CityDTO> EditCity([FromRoute] long id, [FromBody] NewCityRequestDTO newCity)
        {
            City model = newCity.ToModel();
            var cityList = _cityLogic.UpdateCity(id, model);
            return cityList.Select(x => CityDTO.FromModel(x));
        }

        [HttpGet("route/{residentName}/{cityName}")]
        public IActionResult GetFromRoute([FromRoute] string residentName, [FromRoute] string cityName)
        {
            string response = $"Resident {residentName} lives in {cityName} City :: from route";
            return Ok(response);
        }
        [HttpGet("query")]
        public IActionResult GetFromQuery([FromQuery] string residentName, [FromQuery] string cityName)
        {
            string response = $"Resident {residentName} lives in {cityName} City :: from query";
            return Ok(response);
        }
        [HttpPost("body")]
        public IActionResult GetFromBody([FromBody] City name)
        {
            string response = $"Hello from {name.CityName} City!";
            return Ok(response);
        }
    }
}
