using Microsoft.AspNetCore.Mvc;

using RickAndMorty.Web.Attributes;
using RickAndMorty.Web.Core.Services;
using RickAndMorty.Web.Models;
using RickAndMorty.Web.Mappers;

using System.Diagnostics;

namespace RickAndMorty.Web.Controllers
{
    [AddHeader("from-database", "true")]
    public class CharactersController : Controller
    {
        private readonly ILogger<CharactersController> _logger;
        private readonly ICharacterService _characterService;
        private readonly ILocationService _locationService;


        public CharactersController(ILogger<CharactersController> logger, ILocationService locationService, ICharacterService characterService)
        {
            _logger = logger;
            _locationService = locationService;
            _characterService = characterService;
        }

        [HttpGet]
        [Route("characters/{planet?}")]
        [AddHeader("from-database", "true")]
        public async Task<IActionResult> CharactersListAsync(string? planet = default)
        {
            IEnumerable<CharacterViewModel> viewModel = [];

            var characters = await _characterService.GetAllCharactersAsync(planet);

            if(characters is null)
            {
                return View(viewModel);
            }
            
            viewModel = characters.Select(e => e.MapCharacterToViewModel());

            return View(viewModel);
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
