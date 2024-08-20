using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using DeepL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MilkNest.Persistence.Services
{
    public class TranslationService : ITranslationService
    {
        public static readonly string[] Languages = new[] { "en-Us","uk"/*, "es", "fr", "de", "uk", "zh", "ja", "pt-Pt" */};
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITranslator _translator;

        public TranslationService(IHttpContextAccessor contextAccessor, ITranslator translator)
        {
            _contextAccessor = contextAccessor;
            _translator = translator;
        }

        public async Task<List<T>> FillLocalizations<T>(string description, string title) where T : class
        {
            var elementType = typeof(T);
            var result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(T)));

            foreach (var lang in Languages)
            {
                var localization = Activator.CreateInstance(elementType);
                if (localization != null)
                {
                    elementType.GetProperty("Language")?.SetValue(localization, lang);
                    elementType.GetProperty("Title")?.SetValue(localization, await TranslateTextAsync(title, lang));
                    elementType.GetProperty("Description")?.SetValue(localization, await TranslateTextAsync(description, lang));
                    result.Add(localization);
                }
            }
            return result.Cast<T>().ToList();
        }

        public async Task<string> GetCurrentLanguageAsync()
        {
            if (_contextAccessor.HttpContext.Session.TryGetValue("UserSelectedLanguage", out byte[] languageBytes))
            {
                string language = Encoding.UTF8.GetString(languageBytes);
                return language;
            }
            return "uk";
        }

        public async Task<string> TranslateTextAsync(string text, string targetLanguage)
        {
            
            var translationResult = await _translator.TranslateTextAsync(text, null, targetLanguage);
            return translationResult.Text;
        }
    }
}