using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WeatherApp.Models;
using WeatherApp.Models.WeatherModels;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherAppController : Controller
    {

        private readonly IDataService _dataService;

        public WeatherAppController(IDataService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet]
        public IActionResult SearchByCity()
        {
            var SearchCity = new SearchByCityModel();
            return View(SearchCity);
        }

        [HttpPost]
        public IActionResult CitySearching(SearchByCityModel model)
        {
            string? city = model.CityName;
            Coord coord = _dataService.GetCityCoord(city);

            if (coord != null)
            {
                return RedirectToAction("City", "WeatherApp", coord);
            }
            else
            {
                return View("CityNotFound");
            }
        }

        [HttpGet]
        public IActionResult City(Coord coord)
        {
            CityModel viewModel = _dataService.GetForecast(coord);

            if (viewModel != null)
            {
                return View("CityDaily", viewModel);
            }
            else
            {
                return View("CityNotFound");
            }

        }

        [HttpGet]
        public IActionResult SelectSensorData()
        {
            var SelectData = new SelectDataModel();
            return View(SelectData);
        }

        [HttpPost]
        public IActionResult SelectingSensorData(SelectDataModel model)
        {

            return RedirectToAction("SensorData", "WeatherApp", model);

        }

        [HttpGet]
        public IActionResult SensorData(SelectDataModel model)
        {
            var viewModel = _dataService.GetMeasurements(model);
            return View(viewModel);
        }
    }
}
