using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Hubs.ExamPaperTimelineHub
{
    public interface IExamPaperTimelineClient
    {
        Task ReceiveComment(ExamPaperReviewHistoryCommentGetDto eprhc);
    }
}
