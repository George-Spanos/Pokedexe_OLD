using System;
namespace PokedexChat.Extensions {
    public static class DateExtensions {
        public static string MessageFormat(this string isoDate)
        {
            var date = DateTime.Parse(isoDate);
            if (date.Date != DateTime.Today) return date.ToLongDateString();
            return date.Minute == DateTime.Now.Minute ? "Now" : date.ToShortTimeString();
        }
    }
}
