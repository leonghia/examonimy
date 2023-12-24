namespace ExamonimyWeb.Utilities
{
    public readonly struct CommentExamPaperNotiMessage
    {
        public CommentExamPaperNotiMessage(string commenter, string examPaperCode)
        {
            Commenter = commenter;
            ExamPaperCode = examPaperCode;
        }

        public string Commenter { get; }
        public string ExamPaperCode { get; }

        public override string ToString()
        {
            return $"{Commenter} vừa để lại một nhận xét cho đề thi mã {ExamPaperCode} của bạn.";
        }
    }
}
