using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiggerSoft.Model.AiModels
{
        public class ComputerVisionResponse
        {
            [JsonProperty("id")]
            public Guid Id { get; set; }

            [JsonProperty("project")]
            public Guid Project { get; set; }

            [JsonProperty("iteration")]
            public Guid Iteration { get; set; }

            [JsonProperty("created")]
            public DateTimeOffset Created { get; set; }

            [JsonProperty("predictions")]
            public List<Prediction> Predictions { get; set; }
        }

        public class Prediction
        {
            [JsonProperty("probability")]
            public double Probability { get; set; }

            [JsonProperty("tagId")]
            public Guid TagId { get; set; }

            [JsonProperty("tagName")]
            public string TagName { get; set; }
        }


}
