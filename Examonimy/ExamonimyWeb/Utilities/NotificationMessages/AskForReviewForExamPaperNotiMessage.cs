namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class AskForReviewForExamPaperNotiMessage : NotiMessage
    {
        public AskForReviewForExamPaperNotiMessage(string actorFullName, string courseName) : base(actorFullName)
        {
            CourseName = courseName;
        }


        public string CourseName { get; }

        public override string ToVietnamese() => $@"<span class='font-semibold text-gray-700 dark:text-white'>{ActorFullName}</span> vừa tạo một đề thi mới cho môn học <span class='font-semibold text-gray-700 dark:text-white'>{CourseName}</span> và muốn nhờ bạn review.";

        
    }
}
