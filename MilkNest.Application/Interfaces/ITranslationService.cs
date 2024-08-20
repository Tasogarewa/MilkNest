using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface ITranslationService
    {
        Task<string> GetCurrentLanguageAsync();
        Task<string> TranslateTextAsync(string text, string targetLanguage);
        Task<List<T>> FillLocalizations<T>(string description, string title) where T : class;
    }
}
