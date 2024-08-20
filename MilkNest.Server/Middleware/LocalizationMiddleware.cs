using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MilkNest.Server.Middleware
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userSelectedLanguage = context.Session.GetString("UserSelectedLanguage");
            if (!string.IsNullOrEmpty(userSelectedLanguage))
            {
                SetCulture(userSelectedLanguage);
            }
            else
            {
                var acceptLanguageHeader = context.Request.Headers["Accept-Language"].ToString();
                if (!string.IsNullOrEmpty(acceptLanguageHeader))
                {
                    var preferredLanguage = GetPreferredLanguageFromHeader(acceptLanguageHeader);
                    var shortLanguageCode = GetShortLanguageCode(preferredLanguage);

                    SetCulture(shortLanguageCode);

                    context.Session.SetString("UserSelectedLanguage", shortLanguageCode);

                    context.Response.Cookies.Append("UserSelectedLanguage", shortLanguageCode, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                }
                else
                {
                    SetCulture("uk");
                }
            }

            await _next(context);
        }

        private void SetCulture(string languageCode)
        {
            var culture = new CultureInfo(languageCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        private string GetPreferredLanguageFromHeader(string acceptLanguageHeader)
        {
            var languages = acceptLanguageHeader.Split(',')
                .Select(l =>
                {
                    var parts = l.Split(';');
                    var lang = parts[0].Trim();
                    var quality = 1.0;
                    if (parts.Length > 1 && parts[1].StartsWith("q="))
                    {
                        if (double.TryParse(parts[1].Substring(2), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedQuality))
                        {
                            quality = parsedQuality;
                        }
                    }
                    return new { Lang = lang, Quality = quality };
                })
                .OrderByDescending(l => l.Quality)
                .Select(l => l.Lang)
                .ToList();

            return languages.FirstOrDefault() ?? "uk";
        }

        private string GetShortLanguageCode(string cultureName)
        {
            try
            {
                var culture = new CultureInfo(cultureName);
                return culture.TwoLetterISOLanguageName;
            }
            catch
            {
                return "uk";
            }
        }
    }
}