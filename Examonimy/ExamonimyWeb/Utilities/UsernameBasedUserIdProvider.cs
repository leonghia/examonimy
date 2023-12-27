using Microsoft.AspNetCore.SignalR;

namespace ExamonimyWeb.Utilities
{
    public class UsernameBasedUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
