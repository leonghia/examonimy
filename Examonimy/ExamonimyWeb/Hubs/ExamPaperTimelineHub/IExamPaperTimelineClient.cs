using ExamonimyWeb.DTOs.ExamPaperDTO;

namespace ExamonimyWeb.Hubs.ExamPaperTimelineHub
{
    public interface IExamPaperTimelineClient
    {
        Task ReceiveComment(ExamPaperReviewHistoryCommentGetDto eprhc);
        Task ReceiveEdit(ExamPaperReviewHistoryEditGetDto eprhe);
        Task ReceiveApprove(ExamPaperReviewHistoryGetDto eprh);
    }
}
