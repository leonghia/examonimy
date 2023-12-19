using ExamonimyWeb.Utilities;

namespace ExamonimyWeb.Extensions
{
    public static class ExamPaperStatusExtensions
    {
        public static string ToVietnameseString(this ExamPaperStatus? examPaperStatus)
        {
            if (examPaperStatus is null)
                throw new ArgumentNullException(nameof(examPaperStatus));
            return examPaperStatus switch
            {
                ExamPaperStatus.Pending => "Chờ duyệt",
                ExamPaperStatus.Approved => "Đã duyệt",
                _ => throw new ArgumentException(null, nameof(examPaperStatus))
            };
        }

        public static string ToVietnameseString(this ExamPaperStatus examPaperStatus)
        {
            
            return examPaperStatus switch
            {
                ExamPaperStatus.Pending => "Chờ duyệt",
                ExamPaperStatus.Approved => "Đã duyệt",
                _ => throw new ArgumentException(null, nameof(examPaperStatus))
            };
        }
    }
}
