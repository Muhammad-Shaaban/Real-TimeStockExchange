using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RealTimeStockExchange.Helpers.SignalR
{
    // To Use UserId instead of ConnectionId in Communication
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var user = connection.User.FindFirst(ClaimTypes.UserData).Value;
            return user;
        }
    }


    [Authorize]
    public class SignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            SignalRHubConnectionHandler.AddConnection(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            SignalRHubConnectionHandler.RemoveConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }


    // To Use ConnectionId for Communication  
    public static class SignalRHubConnectionHandler
    {
        //Use AddUser and RemoveUser instead of adding items directly
        public static Dictionary<string, string> Connections = new Dictionary<string, string>();

        public static void AddConnection(string connectionId, string userName)
        {
            Connections.Add(connectionId, userName);
            UserConnectedTask?.Invoke(userName);//Invoke it if not null
        }

        public static void RemoveConnection(string connectionId)
        {
            string userName;
            if (Connections.TryGetValue(connectionId, out userName))
            {
                Connections.Remove(connectionId);
                if (!Connections.ContainsValue(userName))
                {
                    UserDisconnectedTask?.Invoke(userName);//Invoke it if not null
                }
            }
        }

        public static Action<string> UserConnectedTask;
        public static Action<string> UserDisconnectedTask;
    }
}
