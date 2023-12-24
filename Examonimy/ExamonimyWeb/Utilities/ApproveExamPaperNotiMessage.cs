namespace ExamonimyWeb.Utilities
{
    public readonly struct ApproveExamPaperNotiMessage
    {
        public string ReviewerFullName { get; }
        public string ExamPaperCode { get; }

        public ApproveExamPaperNotiMessage(string reviewerFullName, string examPaperCode)
        {
            ReviewerFullName = reviewerFullName;
            ExamPaperCode = examPaperCode;
        }

        public override string ToString()
        {
            return $"{ReviewerFullName} đã đồng ý phê duyệt đề thi mã {ExamPaperCode} của bạn.";
        }
    }
}
