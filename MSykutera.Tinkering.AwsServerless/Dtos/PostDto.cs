using System.Text.Json.Serialization;

namespace MSykutera.Tinkering.AwsServerless.Dtos
{
    public class PostDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Content")]
        public string Content { get; set; }
    }
}
