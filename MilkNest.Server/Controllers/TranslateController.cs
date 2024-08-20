using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.Interfaces;

namespace MilkNest.Server.Controllers
{
    public class TranslateController : BaseController
    {
        private readonly ITranslationService _translationService;

        public TranslateController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpPost]
        public async Task<IActionResult> Translate(string text,string targetlanguage)
        {
            Console.WriteLine( await _translationService.GetCurrentLanguageAsync());
            var translationResult = await _translationService.TranslateTextAsync(text, targetlanguage);
            return Ok(translationResult);
        }
    }
}
