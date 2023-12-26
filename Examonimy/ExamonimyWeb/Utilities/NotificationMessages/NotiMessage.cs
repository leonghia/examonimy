namespace ExamonimyWeb.Utilities.NotificationMessages
{
    public abstract class NotiMessage
    {
        public string ActorFullName { get; init; }

        public NotiMessage(string actorFullName)
        {
            ActorFullName = actorFullName;
        }

        public abstract string ToVietnamese();
    }
}
