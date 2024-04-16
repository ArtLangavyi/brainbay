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

            var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Get All Planets", "GET");

            int newCharacterId = 0;
            try
            {
                var request = model.MapAddCharacterViewModelToAddCharacterRequest();

                newCharacterId = await _characterService.AddCharacterAsync(request);

                if (newCharacterId == 0)
                {
                    ModelState.AddModelError("Name", "Huston, we have a problem!");

                    apmTransaction.CaptureError($"Couldn't save new character with name: {model.Name}", nameof(AddCharacterAsync), null);
                }
            }
            catch (Exception ex)
            {
                apmTransaction.CaptureException(ex);
            }
            finally
            {
                apmTransaction.End();
            }


            return View("CharacterCreated", newCharacterId);
        }
    }
}
