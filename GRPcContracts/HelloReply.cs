using System.Runtime.Serialization;

namespace GRPcContracts
{
    [DataContract]
    public class HelloReply
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}