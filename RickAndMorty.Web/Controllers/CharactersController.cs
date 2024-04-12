using Microsoft.AspNetCore.Mvc;

using RickAndMorty.Web.Attributes;
using RickAndMorty.Web.Core.Services;
using RickAndMorty.Web.Mappers;
using RickAndMorty.Web.Models;

namespace RickAndMorty.Web.Controllers
{
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
        [AddHeader("from-database", "true")]
        public async Task<IActionResult> CharactersListAsync(string? planet = default)
        {
            IEnumerable<CharacterViewModel> viewModel = [];

            var characters = await _characterService.GetAllCharactersAsync(planet);

            if (characters is null)
            {
                return View(viewModel);
            }

            viewModel = characters.Select(e => e.MapCharacterToViewModel());

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddCharacterAsync()
        {
            var viewModel = new AddCharacterViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacterAsync(AddCharacterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapAddCharacterViewModelToAddCharacterRequest();

            var newCharacterId = await _characterService.AddCharacterAsync(request);

            if (newCharacterId == 0)
            {
                ModelState.AddModelError("Name", "Huston, we have a problem!");
            }

            return View(model);
        }
    }
}
