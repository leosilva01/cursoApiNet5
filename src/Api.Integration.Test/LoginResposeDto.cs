using System;
using Newtonsoft.Json;

namespace Api.Integration.Test
{
    public class LoginResposeDto
    {
        [JsonProperty ("authenticated")]
        public bool authenticated { get; set; }
        
        [JsonProperty ("created")]
        public DateTime created { get; set; }
        
        [JsonProperty ("expiration")]
        public DateTime expiration { get; set; }
        
        [JsonProperty ("accessToken")]
        public string accessToken { get; set; }
        
        [JsonProperty ("userEmail")]
        public string userEmail { get; set; }
        
        [JsonProperty ("message")]
        public string message { get; set; }
    }
}