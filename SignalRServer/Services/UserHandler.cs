using System.Collections.Generic;
using System.Linq;

namespace SignalRServer.Services
{
    public static class UserHandler
    {
        private static readonly List<UserAgent> ConnectionIds = new List<UserAgent>();

        public static void RemoveAgent(string id) => ConnectionIds.Remove(FindAgent(id));

        public static void AddAgent(UserAgent agent) => ConnectionIds.Add(agent);

        public static UserAgent FindAgent(string name) => ConnectionIds.Where(x => x.nickname == name).FirstOrDefault();

        public static List<UserAgent> AllAgentsInDescendingOrder() => ConnectionIds.OrderByDescending(x => x.nickname).ToList();

        public static bool ChangeNickname(string connection, string newName)
        {
            var agent = ConnectionIds.FirstOrDefault(x => x.connectionId == connection);
            if (agent == null)
                return false;

            var oldIndex = ConnectionIds.IndexOf(agent);
            var newAgent = new UserAgent(connection, newName);
            ConnectionIds.RemoveAt(oldIndex);
            ConnectionIds.Insert(oldIndex, newAgent);
            return true;
        }
    }

    public record UserAgent(string connectionId, string nickname);
}