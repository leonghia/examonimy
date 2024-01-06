namespace ExamonimyWeb.Utilities.NotificationMessages;

public class ChangedExamScheduleNotiMessage : NotiMessage
{
    private readonly string _courseName;

    public ChangedExamScheduleNotiMessage(string actorFullName, bool isRead, string courseName) : base(actorFullName, isRead)
    {
        _courseName = courseName;
    }

    public override string ToVietnamese()
    {
        if (IsRead)
            return $@"<div class='text-gray-500 text-sm mb-1.5'>Lịch thi môn học <span class='font-semibold text-gray-600'>{_courseName}</span> của bạn đã có sự thay đổi.</div>";
        else
            return $@"<div class='text-gray-600 text-sm mb-1.5'>Lịch thi môn học <span class='font-semibold text-gray-700'>{_courseName}</span> của bạn đã có sự thay đổi.</div>";
    }
}
