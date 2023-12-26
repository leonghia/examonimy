namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class CommentExamPaperNotiMessage : NotiMessage
    {
        public CommentExamPaperNotiMessage(string actorFullName, string examPaperCode) : base(actorFullName)
        {
            ExamPaperCode = examPaperCode;
        }

        public string ExamPaperCode { get; }            

        public override string ToVietnamese()
        {
            return $"{ActorFullName} vừa để lại một nhận xét cho đề thi mã {ExamPaperCode} của bạn.";
        }
    }
}
