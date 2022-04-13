using System.Text.Json.Serialization;

namespace Azmoon.ElFinder.Models.Commands
{
    public class GetResponseModel
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}