﻿namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class RejectExamPaperNotiMessage : NotiMessage
    {
        public string ExamPaperCode { get; }

        public RejectExamPaperNotiMessage(string actorFullName, string examPaperCode, bool isRead) : base(actorFullName, isRead)
        {
            ExamPaperCode = examPaperCode;
        }      

        public override string ToVietnamese()
        {
            throw new NotImplementedException();
        }
    }
}
