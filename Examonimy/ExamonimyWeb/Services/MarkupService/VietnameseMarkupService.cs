using ExamonimyWeb.Enums;
using ExamonimyWeb.Extensions;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Services.MarkupService;

public class VietnameseMarkupService : IMarkupService
{
    public string GetDateTimeAgoMarkup(DateTime date, bool isRead)
    {
        var (Ago, Amount) = date.GetDateTimeAgo();
        var amount = Convert.ToInt32(Amount);

        var textContent = Ago switch
        {
            DateTimeAgo.MomentAgo => "vừa mới đây",
            DateTimeAgo.SecondsAgo => "vài giây trước",
            DateTimeAgo.MinutesAgo => $"{amount} phút trước",
            DateTimeAgo.HoursAgo => $"{amount} giờ trước",
            DateTimeAgo.DaysAgo => $"{amount} ngày trước",
            DateTimeAgo.WeeksAgo => $"{amount} tuần trước",
            DateTimeAgo.YearsAgo => $"{amount} năm trước",
            _ => throw new SwitchExpressionException(Ago)
        };
        if (isRead)
            return $@"<div class='text-xs text-gray-400 font-normal'>{textContent}</div>";
        return $@"<div class='text-xs text-blue-600 font-medium flex items-center justify-between'><span>{textContent}</span><div class='flex-none rounded-full p-1 text-blue-500 bg-blue-500/10'><div class='h-2 w-2 rounded-full bg-current'></div></div></div>";
    }
}
