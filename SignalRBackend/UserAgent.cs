using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBackend
{
    public class UserAgent
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; } = new HashSet<string>();

        public UserAgent(string name, string connectionIds)
        {
            Name = name;
            ConnectionIds.Add(connectionIds);
        }
    }
}