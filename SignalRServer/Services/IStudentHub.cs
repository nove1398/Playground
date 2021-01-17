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
    }
}