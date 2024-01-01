namespace ExamonimyWeb.Utilities.NotificationMessages;

public class EditExamPaperNotiMessage : NotiMessage
{
    public EditExamPaperNotiMessage(string actorFullName, bool isRead, string examPaperCode) : base(actorFullName, isRead)
    {
        ExamPaperCode = examPaperCode;
    }

    public string ExamPaperCode { get; }

    public override string ToVietnamese()
    {
        if (IsRead)
            return $@"<div class='text-gray-500 text-sm mb-1.5'><span class='font-semibold text-gray-600'>{ActorFullName}</span> đã cập nhật đề thi mã <span class='font-semibold text-gray-600'>{ExamPaperCode}</span>.</div>";
        else
            return $@"<div class='text-gray-600 text-sm mb-1.5'><span class='font-semibold text-gray-700'>{ActorFullName}</span> đã cập nhật đề thi mã <span class='font-semibold text-gray-700'>{ExamPaperCode}</span>.</div>";
    }
}
