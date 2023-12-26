namespace ExamonimyWeb.Services.MarkupService
{
    public interface IMarkupService
    {
        string GetDateTimeAgoMarkup(DateTime date, bool isRead);
    }
}
