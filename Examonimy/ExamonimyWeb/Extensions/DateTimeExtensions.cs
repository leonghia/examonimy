using ExamonimyWeb.Enums;

namespace ExamonimyWeb.Extensions
{
    public static class DateTimeExtensions
    {
        public static (DateTimeAgo Ago, double Amount) GetDateTimeAgo(this DateTime dateTime)
        {
            var ts = DateTime.UtcNow - dateTime;
            if (ts.TotalSeconds < 10)
                return (DateTimeAgo.MomentAgo, ts.TotalSeconds);
            if (ts.TotalMinutes < 1)
                return (DateTimeAgo.SecondsAgo, ts.TotalSeconds);
            if (ts.TotalHours < 1)
                return (DateTimeAgo.MinutesAgo, ts.TotalMinutes);
            if (ts.TotalDays < 1)
                return (DateTimeAgo.HoursAgo, ts.TotalHours);
            if (ts.TotalDays < 7)
                return (DateTimeAgo.DaysAgo, ts.TotalDays);
            if (ts.TotalDays < 365)
                return (DateTimeAgo.WeeksAgo, ts.TotalDays / 7);
            return (DateTimeAgo.YearsAgo, ts.TotalDays / 365.2425);
        }
    }
}
