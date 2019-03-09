using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ExcavatorCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TempController : ControllerBase
    {
        private const string KeyId = "xxx";
        private const string Secret = "xxx";
        private const string Email = "xxx@xxx.xxx.xxx.com";

        private const string BaseUrl = "xxxx";
        private const string ApiPath = "/v2/projects/{0}/devices/{1}/events";

        private const string OauthUrl = "https://identity.disruptive-technologies.com/oauth2/token";

        private const string ProjectId = "xxx";
        private const string TempSensorA = "xxx";

        public async Task<IActionResult> Temp()
        {
            var token = await GetOAuthToken();

            var client = HttpClientFactory.Create();
            client.BaseAddress = new Uri(BaseUrl);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var path = string.Format(ApiPath, ProjectId, TempSensorA);
            var response = await client.GetAsync(new Uri(path, UriKind.Relative));

            var sensorData = await response.Content.ReadAsStringAsync();

            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<DissruptivePayload>(sensorData);

            list.Events = list.Events.Where(x => x.EventType == "temperature").ToArray();


            //ViewBag.SensorData = sensorData;
            //ViewBag.Events = list.Events;

            return Ok(list.Events);
        }

        private async Task<string> GetOAuthToken()
        {
            var now = DateTime.Now;

            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), "HS256");

            var header = new JwtHeader(signingCredentials) { { "kid", KeyId } };

            var payload = new JwtPayload(Email, OauthUrl, null, null, now.AddMinutes(30), now);

            var jwt = new JwtSecurityToken(header, payload);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var client = HttpClientFactory.Create();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("assertion", encodedJwt),
                new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"),
            });

            var result = await client.PostAsync(new Uri(OauthUrl), content);

            if (result.IsSuccessStatusCode)
            {
                var resultPayload = await result.Content.ReadAsStringAsync();

                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(resultPayload);
                var token = json.access_token.ToString();

                return token;
            }

            return null;
        }
    }

    public class Labels
    {
        [JsonProperty("temp")]
        public string Temp { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("press")]
        public string Press { get; set; }
        [JsonProperty("prox")]
        public string Prox { get; set; }
    }

    public class DissruptivePayload
    {
        [JsonProperty("event")]
        public Event Event { get; set; }

        [JsonProperty("labels")]
        public Labels Labels { get; set; }



        //For API
        [JsonProperty("events")]
        public Event[] Events { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }
    }

    public class Event
    {
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }

    public class Data
    {
        [JsonProperty("temperature")]
        public Temperature Temperature { get; set; }

        [JsonProperty("objectPresent")]
        public ObjectPresent ObjectPresent { get; set; }
    }

    public class Temperature
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("updateTime")]
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class ObjectPresent
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("updateTime")]
        public DateTimeOffset UpdateTime { get; set; }
    }
}