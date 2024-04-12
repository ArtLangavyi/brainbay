using Microsoft.AspNetCore.Mvc;

using RickAndMorty.Web.Core.Services;
using RickAndMorty.Web.Models;

using System.Diagnostics;

namespace RickAndMorty.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICharacterService _characterService;
        private readonly ILocationService _locationService;


        public HomeController(ILogger<HomeController> logger, ILocationService locationService, ICharacterService characterService)
        {
            _logger = logger;
            _locationService = locationService;
            _characterService = characterService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var planets = await _locationService.GetAllPlanetsAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
