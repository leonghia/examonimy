namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class ApproveExamPaperNotiMessage : NotiMessage
    {

        public string ExamPaperCode { get; }

        public ApproveExamPaperNotiMessage(string actorFullName, string examPaperCode) : base(actorFullName)
        {
            ExamPaperCode = examPaperCode;
        }       

        

        public override string ToVietnamese()
        {
            throw new NotImplementedException();
        }
    }
}
