using System;
using System.Threading.Tasks;

namespace APITwo
{
    public interface IBusHub
    {
        public Task<string> Value();
    }

    public class BusHub : IBusHub
    {
        public BusHub()
        {
        }

        public async Task<string> Value()
        {
            await Task.Delay(2000);
            return $"{nameof(BusHub)}";
        }
    }
}