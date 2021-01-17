using SignalRServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public interface IStudentService
    {
        event EventHandler<StudentObject> StudentUpdate;

        Task RunAsync(CancellationToken cts);
    }
}