using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetsGoPark.WebSite.Models
{
    //This class defines the attributes that a Park has, and is used to grab and use information from parks.json
    public class ParksModel
    {
        //A string Id that correlates to the park's name.
        public string Id { get; set; }
        //The name of the park
        public string Name { get; set; }
        //The URL that corresponds to the park's image.
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        //Park title (sometimes different from name.
        public string Title { get; set; }
        //Paragraph description of the park and its features.
        public string Description { get; set; }
        //int array of ratings (0-5 allowed). Is null if a park has no ratings.
        public int[] Ratings { get; set; }
        //Address of the park.
        public string Address { get; set; }
        //Phone number of the park's owner or governing agency.
        public string Phone { get; set; }
        //Which system (city, state, federal) that the park belongs to 
        public string Park_system { get; set; }
        //A list of strings highlighting activities that can be done at the park.
        public string[] Activities { get; set; }
        //URL to the parks agency provided brochure
        public string Map_brochure { get; set; }
        //permits or fees required to access the park.
        public string Permits { get; set; }
        //A 2D string array of the comments on the park.
        public string[][] Comments { get; set; }
    }
}