using ExamonimyWeb.Enums;

namespace ExamonimyWeb.Utilities
{
    public class RequestParamsForExamPaper : RequestParams
    {
        public int? CourseId { get; set; }
        public ExamPaperStatus? Status { get; set; }
    }
}
