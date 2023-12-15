namespace ExamonimyWeb.Utilities
{
    public class QuestionRequestParams : RequestParams
    {
        public int? QuestionTypeId { get; set; }
        public int? QuestionLevelId { get; set; }
        public int? CourseId { get; set; }
    }
}
