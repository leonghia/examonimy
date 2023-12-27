namespace ExamonimyWeb.Extensions;

public static class DateTimeExtensions
{

    private const int _defaultTimezoneOffset = 0;
    private const int _offsetMultiplier = -1;

    public static DateTime ConvertTo(this DateTime dateTime, string? timezoneOffsetStr)
    {                   
        DateTime createdAt;
        if (timezoneOffsetStr is null)
            createdAt = dateTime.AddMinutes(Convert.ToDouble(_defaultTimezoneOffset) * _offsetMultiplier);
        else
            createdAt = dateTime.AddMinutes(Convert.ToDouble(int.Parse(timezoneOffsetStr)) * _offsetMultiplier);
        return createdAt;
    }
}
