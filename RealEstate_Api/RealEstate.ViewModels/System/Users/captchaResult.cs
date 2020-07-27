using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class captchaResult
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        [JsonProperty("error-codes")]
        public List<string> errors { get; set; }
    }
}
