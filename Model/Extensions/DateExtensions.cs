using System;
namespace Model.Extensions {
    public static class DateExtensions {
        public static string MessageFormat(this DateTime date)
        {
            if (date.Date != DateTime.Today) return date.ToLongDateString();
            return date.Minute == DateTime.Now.Minute ? "Now" : date.ToShortTimeString();
        }
    }
}
