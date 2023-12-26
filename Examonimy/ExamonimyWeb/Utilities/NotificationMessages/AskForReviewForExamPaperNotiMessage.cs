namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public class AskForReviewForExamPaperNotiMessage : NotiMessage
    {
        public AskForReviewForExamPaperNotiMessage(string actorFullName, string courseName, bool isRead) : base(actorFullName, isRead)
        {
            CourseName = courseName;
        }


        public string CourseName { get; }

        public override string ToVietnamese()
        {
            if (IsRead)
                return $@"<div class='text-gray-400 text-sm mb-1.5'><span class='font-semibold text-gray-400'>{ActorFullName}</span> đã tạo một đề thi mới cho môn học <span class='font-semibold text-gray-400'>{CourseName}</span> và muốn nhờ bạn review.</div>";
            else
                return $@"<div class='text-gray-500 text-sm mb-1.5'><span class='font-semibold text-gray-700'>{ActorFullName}</span> đã tạo một đề thi mới cho môn học <span class='font-semibold text-gray-700'>{CourseName}</span> và muốn nhờ bạn review.</div>";
        }

        
    }
}
