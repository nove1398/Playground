using System.Threading.Tasks;

namespace SignalRBackend.Hubs
{
    public interface IChatHub
    {
        Task LoungeMessage(string user, string message);

        Task NameChanged(string oldName, string newName);

        Task PrivateMessage(string fromUser, string message);
    }
}