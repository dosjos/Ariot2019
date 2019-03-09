using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiggerSoft
{
    public class MoveRobot
    {
        private static HttpClient _client;
        private int _factor = 1;
        public MoveRobot(int factor)
        {
            if(_client == null)
            {
                _client = new HttpClient();
            }
            _factor = factor;
        }

        public async Task Left()
        {
            await _client.GetAsync("https://excavator.azurewebsites.net/api/left");
            Thread.Sleep(800 * _factor);
            await _client.GetAsync("https://excavator.azurewebsites.net/api/forward");
            Thread.Sleep(1500 * _factor);
            await _client.GetAsync("https://excavator.azurewebsites.net/api/stop");
        }

        public async Task Right()
        {
            await _client.GetAsync("https://excavator.azurewebsites.net/api/right");
            Thread.Sleep(800 * _factor);
            await _client.GetAsync("https://excavator.azurewebsites.net/api/forward");
            Thread.Sleep(1500 * _factor);
            await _client.GetAsync("https://excavator.azurewebsites.net/api/stop");
        }

        public async Task Forward()
        {
            await _client.GetAsync("https://excavator.azurewebsites.net/api/forward");
            Thread.Sleep(1500 * _factor);
            await _client.GetAsync("https://excavator.azurewebsites.net/api/stop");
        }
    }
}
