namespace ExamonimyWeb.Utilities
{
    public readonly struct AskForReviewForExamPaperNotiMessage
    {
        public AskForReviewForExamPaperNotiMessage(string authorFullName, string courseName)
        {
            AuthorFullName = authorFullName;
            CourseName = courseName;
        }

        public string AuthorFullName { get; }
        public string CourseName { get; }

        public override string ToString()
        {
            return $"{AuthorFullName} vừa tạo một đề thi mới cho môn học {CourseName} và muốn nhờ bạn review.";
        }
    }
}
