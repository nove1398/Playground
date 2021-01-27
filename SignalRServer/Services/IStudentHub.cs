using SignalRServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public interface IStudentHub
    {
        Task SendStudentInfo(StudentObject student);

        Task SendBroadcastMessage(string message);

        Task UpdateNickname(string newName);

        Task SendParticipants(List<string> participants);

        Task SendMessage(string message);

        Task SendUserlist(List<UserAgent> names);
    }
}