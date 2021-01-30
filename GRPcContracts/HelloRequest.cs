using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.Serialization;

namespace GAPI2.Controllers
{
    [DataContract]
    public class HelloRequest
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public byte[] FileName { get; set; }
    }
}