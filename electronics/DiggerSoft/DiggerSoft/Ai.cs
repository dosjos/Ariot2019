using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DiggerSoft.Model.AiModels;

namespace DiggerSoft
{
    public class Ai
    {
        private static HttpClient _client;

        private const string Url = "xxx";

        public Ai()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Prediction-Key", "xxx");
            }
        }

        public async Task<bool> IsImageValidTarget(byte[] pictureBytes)
        {
            if (pictureBytes == null || pictureBytes.Length == 0)
            {
                Console.WriteLine("AI: Error! No image!");
                return false;
            }

            var content = new MultipartFormDataContent
            {
                new ByteArrayContent(pictureBytes)
            };

            ComputerVisionResponse response;
            try
            {
                var result = await _client.PostAsync(Url, content);
                response = await result.Content.ReadAsAsync<ComputerVisionResponse>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"AI: Error! Call to API failed. Or parsing failed. Message: {e.Message}");
                return false;
            }

            foreach (var prediction in response.Predictions)
            {
                Console.WriteLine($"{prediction.TagName.PadRight(6)}: {prediction.Probability:P1}");
            }

            var best = response.Predictions.OrderByDescending(p => p.Probability).FirstOrDefault();

            if (best == null)
            {
                Console.WriteLine("AI: Error! No best response.");
                return false;
            }

            Console.WriteLine($"AI says: Best guess: {best.TagName.PadLeft(6)}! {best.Probability:P1} sure.");

            switch (best.TagName)
            {
                case "Negative":
                    return false;
                case "dig":
                    return true;
                default:
                    Console.WriteLine($"AI: Error: Unknown tag {best.TagName}.");
                    return false;
            }
        }
}
}
