namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public abstract class NotiMessage
    {
        public string ActorFullName { get; init; }
        public bool IsRead { get; init; }

        public NotiMessage(string actorFullName, bool isRead)
        {
            ActorFullName = actorFullName;
            IsRead = isRead;
        }

        public abstract string ToVietnamese();
    }
}
