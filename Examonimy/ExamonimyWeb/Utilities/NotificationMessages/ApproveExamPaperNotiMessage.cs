namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class ApproveExamPaperNotiMessage : NotiMessage
    {

        public string ExamPaperCode { get; }

        public ApproveExamPaperNotiMessage(string actorFullName, string examPaperCode, bool isRead) : base(actorFullName, isRead)
        {
            ExamPaperCode = examPaperCode;
        }       

        

        public override string ToVietnamese()
        {
            if (IsRead)
                return $@"<div class='text-gray-500 text-sm mb-1.5'><span class='font-semibold text-gray-600'>{ActorFullName}</span> đã đồng ý phê duyệt đề thi mã <span class='font-semibold text-gray-600'>{ExamPaperCode}</span> của bạn.</div>";
            else
                return $@"<div class='text-gray-600 text-sm mb-1.5'><span class='font-semibold text-gray-700'>{ActorFullName}</span> đã đồng ý phê duyệt đề thi mã <span class='font-semibold text-gray-700'>{ExamPaperCode}</span> của bạn.</div>";
        }
    }
}
