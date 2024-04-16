using Elastic.Apm.Api;

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
        public async Task<IActionResult> CharactersListAsync(int pageNumber = 1, string? planet = default)
        {
            var characterResponseBase = await _characterService.GetAllCharactersAsync(pageNumber, planet);

            if (characterResponseBase is null)
            {
                return View();
            }
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

            var viewModel = characterResponseBase.MapCharacterResponseBaseToCharactersListViewModel(baseUrl);
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

                    _logger.LogError("Couldn't save new character with name: {name}", model.Name);
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
