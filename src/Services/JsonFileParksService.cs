using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LetsGoPark.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace LetsGoPark.WebSite.Services
{
    //This class is used by the website do deserialize the Json file and put it in a usable format for our website.
   public class JsonFileParksService
    {
        //This function determines the environment of the webhost that is using the class's service.
        public JsonFileParksService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        //Allows the ability to get the used web host environment.
        public IWebHostEnvironment WebHostEnvironment { get; }

        //This value stores the name of the json file where the park data is located.
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "parks.json"); }
        }

        //Function returns a IEnumerable container of all parks found within the json file. 
        public IEnumerable<ParksModel> GetParks()
        {
            //Opens the json file
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                //returns a deserialized version of the JSON divided into seperate parksModel objects defined in ParksModel.cs.
                return JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        //Sets case sensitivity prefererence for the file.
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        //Function returns only the Highest Rated Park found within the json file.
        //If two parks have the same rating, it returns the park with the highest vote count.
        public ParksModel GetHighestRatedPark()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                IEnumerable<ParksModel> Parks = JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                ParksModel topPark = Parks.FirstOrDefault();
                foreach (var park in Parks)
                {
                    //If the current top rated park has no ratings, the current park is considered the top park.
                    if (topPark.Ratings == null) { topPark = park; }
                    //If the current park is null, topPark is unchanged.
                    else if (park.Ratings == null) { continue; }
                    //If both parks have the same rating, the park with the higher number of votes is the new top park.
                    else if (park.Ratings != null && topPark.Ratings.Average() == park.Ratings.Average())
                    {
                        //If the count rating of the current park is higher, the current park is the new top park.
                        if (park.Ratings != null && topPark.Ratings.Count() < park.Ratings.Count())
                            topPark = park;
                    }
                    //If the current park has a higher average rating than the top park, the current is the new top park.
                    else if (topPark.Ratings.Average() < park.Ratings.Average())
                    {
                        topPark = park;
                    }
                }
                return topPark;
            }
        }

        //This function adds a rating to a park defined by the argument ParkId.
        public void AddRating(string ParkId, int rating)
        {
            //Gets all parks in the json file
            var Parks = GetParks();

            //If the current park has no current ratings, creates a int array and inputs the passed in rating as its first value.
            if(Parks.First(x => x.Id == ParkId).Ratings == null)
            {
                //Sets the first value at index 0 in the ratings array.
                Parks.First(x => x.Id == ParkId).Ratings = new int[] { rating };
            }
            else
            {
                //Joins the current rating to the existing array of ratings.
                var ratings = Parks.First(x => x.Id == ParkId).Ratings.ToList();
                ratings.Add(rating);
                Parks.First(x => x.Id == ParkId).Ratings = ratings.ToArray();
            }

            //Saves the updated rating to the json file.
            using(var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ParksModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }), 
                    Parks
                );
            }
        }
    }
}