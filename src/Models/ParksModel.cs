using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetsGoPark.WebSite.Models
{
    public class ParksModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Park_system { get; set; }
        public string[] Activites { get; set; }
        public string Map_brochure { get; set; }
        public string Permits { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ParksModel>(this);

 
    }
}