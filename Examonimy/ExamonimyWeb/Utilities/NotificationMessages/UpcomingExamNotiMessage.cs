namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class UpcomingExamNotiMessage : NotiMessage
    {
        private readonly string _courseName;

        public UpcomingExamNotiMessage(string actorFullName, bool isRead, string courseName) : base(actorFullName, isRead)
        {
            _courseName = courseName;
        }

        public override string ToVietnamese()
        {
            if (IsRead)
                return $@"<div class='text-gray-500 text-sm mb-1.5'>Bạn có lịch thi mới cho môn học <span class='font-semibold text-gray-600'>{_courseName}</span>.</div>";
            else
                return $@"<div class='text-gray-600 text-sm mb-1.5'>Bạn có lịch thi mới cho môn học <span class='font-semibold text-gray-700'>{_courseName}</span>.</div>";
        }
    }
}
