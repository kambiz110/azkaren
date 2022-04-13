using System.Text.Json.Serialization;

namespace Azmoon.ElFinder.Models.Commands.Open
{
    public class DebugResponseModel
    {
        [JsonPropertyName("connector")]
        public string Connector => ".net";
    }
}