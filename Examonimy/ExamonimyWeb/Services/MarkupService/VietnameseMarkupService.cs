using ExamonimyWeb.Enums;
using ExamonimyWeb.Extensions;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Services.MarkupService;

public class VietnameseMarkupService : IMarkupService
{
    public string GetDateTimeAgoMarkup(DateTime date)
    {
        var tuple = date.GetDateTimeAgo();
        var amount = Convert.ToInt32(tuple.Amount);
        return tuple.Ago switch
        {
            DateTimeAgo.MomentAgo => "vừa mới đây",
            DateTimeAgo.SecondsAgo => "vài giây trước",
            DateTimeAgo.MinutesAgo => $"{amount} phút trước",
            DateTimeAgo.HoursAgo => $"{amount} giờ trước",
            DateTimeAgo.DaysAgo => $"{amount} ngày trước",
            DateTimeAgo.WeeksAgo => $"{amount} tuần trước",
            DateTimeAgo.YearsAgo => $"{amount} năm trước",
            _ => throw new SwitchExpressionException(tuple.Ago)
        };
    }
}
