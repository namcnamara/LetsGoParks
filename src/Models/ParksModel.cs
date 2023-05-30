using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetsGoPark.WebSite.Models
{
    //This class defines the attributes that a Park has, and is used to grab and use information from parks.json
    public class ParksModel
    {
        //A string Id that correlates to the park's name.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        public string Id { get; set; }

        
        //The Image that corresponds to the park's image.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        [RegularExpression(@"^(http:\/\/|https:\/\/).*", ErrorMessage = "The {0} must start with 'http://' or 'https://'")]
        [JsonPropertyName("img")]
        public string Image { get; set; }
        
        //Url to park's official website
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        [RegularExpression(@"^(http:\/\/|https:\/\/).*", ErrorMessage = "The {0} must start with 'http://' or 'https://'")]
        public string Url { get; set; }

        
        //Paragraph description of the park and its features.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 1000, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        public string Description { get; set; }

        //int array of ratings (0-5 allowed). Is null if a park has no ratings.
        public int[] Ratings { get; set; }

        //Address of the park.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        public string Address { get; set; }

        //Phone number of the park's owner or governing agency.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The {0} must be a numeric value.")] 
        public string Phone { get; set; }

        //Which system (state, federal, city) that the park belongs to 
        public ParkSystemEnum Park_system { get; set; } 

        //A list of strings highlighting activities that can be done at the park.
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        public string Activities { get; set; }

        //URL to the parks agency provided brochure
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The {0} should have a length of more than {2} and less than {1}")]
        public string Map_brochure { get; set; }

        //permits or fees required to access the park.
        [Required(ErrorMessage = "{0} is required!")]
        public string Permits { get; set; }

        //A 2D string array of the comments on the park.
        public string[][] Comments { get; set; }
    }
}