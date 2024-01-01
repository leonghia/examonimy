namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class CommentExamPaperNotiMessage : NotiMessage
    {
        public CommentExamPaperNotiMessage(string actorFullName, string examPaperCode, bool isRead) : base(actorFullName, isRead)
        {
            ExamPaperCode = examPaperCode;
        }

        public string ExamPaperCode { get; }            

        public override string ToVietnamese()
        {
            if (IsRead)
                return $@"<div class='text-gray-500 text-sm mb-1.5'><span class='font-semibold text-gray-600'>{ActorFullName}</span> đã nhận xét về đề thi mã <span class='font-semibold text-gray-600'>{ExamPaperCode}</span> của bạn.</div>";
            else
                return $@"<div class='text-gray-600 text-sm mb-1.5'><span class='font-semibold text-gray-700'>{ActorFullName}</span> đã nhận xét về đề thi mã <span class='font-semibold text-gray-700'>{ExamPaperCode}</span> của bạn.</div>";
        }
    }
}
