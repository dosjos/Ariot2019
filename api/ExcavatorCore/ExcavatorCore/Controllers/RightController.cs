using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExcavatorCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RightController : ControllerBase
    {
     
        public async Task<IActionResult> Forward()
        {
            var mqttClient = await MqttClient.CreateAsync("sss");

            var sess = await mqttClient.ConnectAsync(new MqttClientCredentials("xxx", "xxx", "xxx"));

            string sendTopic = "movement";

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject("d"));

            await mqttClient.PublishAsync(new MqttApplicationMessage(sendTopic, data), MqttQualityOfService.ExactlyOnce);

            return Ok();
        }
    }
}