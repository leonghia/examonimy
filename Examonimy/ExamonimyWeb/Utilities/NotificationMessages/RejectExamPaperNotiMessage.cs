namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class RejectExamPaperNotiMessage : NotiMessage
    {
        public string ExamPaperCode { get; }

        public RejectExamPaperNotiMessage(string actorFullName, string examPaperCode) : base(actorFullName)
        {
            ExamPaperCode = examPaperCode;
        }      

        public override string ToVietnamese()
        {
            throw new NotImplementedException();
        }
    }
}
