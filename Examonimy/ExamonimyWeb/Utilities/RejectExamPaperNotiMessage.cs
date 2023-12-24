namespace ExamonimyWeb.Utilities
{
    public readonly struct RejectExamPaperNotiMessage
    {
        public string ReviewerFullName { get; }
        public string ExamPaperCode { get; }

        public RejectExamPaperNotiMessage(string reviewerFullName, string examPaperCode)
        {
            ReviewerFullName = reviewerFullName;
            ExamPaperCode = examPaperCode;
        }

        public override string ToString()
        {
            return $"{ReviewerFullName} đã từ chối phê duyệt đề thi mã {ExamPaperCode} của bạn.";
        }
    }
}
