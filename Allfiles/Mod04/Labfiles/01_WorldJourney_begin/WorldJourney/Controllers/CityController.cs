using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WorldJourney.Models;

namespace WorldJourney.Controllers
{
    public class CityController : Controller
    {
        private IData _data;
        private IHostingEnvironment _environment;

        public CityController(IData data, IHostingEnvironment enviroment)
        {
            _data = data;
            _environment = enviroment;
            _data.CityInitializeData();
        }

        public IActionResult Index()
        {
            ViewData["Page"] = "Search city";
            return View();
        }

        public IActionResult Details(int? id)
        {
            ViewData["Page"] = "Selected City";
            City city = _data.GetCityById(id);
            if (city == null)
                return NotFound();
            ViewBag.Title = city.CityName;
            return View(city);
        }

        public IActionResult GetImage(int? cityId)
        {
            ViewData["Message"] = "Display Image";
            City requestedCity = _data.GetCityById(cityId);
            if (requestedCity == null)
                return NotFound();

            string webRootpath = _environment.WebRootPath;
            string folderPath = "\\images\\";
            string fullPath = $"{webRootpath}{folderPath}{requestedCity.ImageName}";
            FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
            byte[] fileBytes;
            using(BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return File(fileBytes, requestedCity.ImageMimeType);
        }
    }
}
