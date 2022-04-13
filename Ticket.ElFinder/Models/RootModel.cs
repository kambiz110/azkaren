using System.Text.Json.Serialization;

namespace Azmoon.ElFinder.Models
{
    public class RootModel : DirectoryModel
    {
        [JsonPropertyName("isroot")]
        public byte IsRoot => 1;
    }
}