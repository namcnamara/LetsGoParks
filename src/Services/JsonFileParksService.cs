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
                //Uses the compareParks() function to select the highest rated park from parks.json
                foreach (var park in Parks)
                {
                    topPark = compareParks(topPark, park);
                }
                return topPark;
            }
        }

        //This function returns an array of size 3 that holds the top city, state, and national park by rating. 
        public ParksModel[] GetHighestRatedParks()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                IEnumerable<ParksModel> Parks = JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                //topRatedParks is an array that holds the 3 highest rated parks for each catagory.
                //Index 0 is for city parks, index 1 is for state parks, and index 2 is for national parks.
                ParksModel[] topRatedParks = { Parks.FirstOrDefault(), Parks.FirstOrDefault(), Parks.FirstOrDefault()};
                foreach (var park in Parks)
                {
                    switch(park.Park_system)
                    {
                        //Compares top city park against potential contenders.
                        case "City Parks":
                            //If the current top city park doesn't actually belong to the city system, switch it with the current park.
                            if (topRatedParks != null && topRatedParks[0].Park_system != "City Parks")
                                topRatedParks[0] = park;
                            else
                                topRatedParks[0] = compareParks(topRatedParks[0], park);
                            break;
                        //Compares top state park against potential contenders.
                        case "WA State Parks":
                            //If the current top state park doesn't actually belong to the state system, switch it with the current park.
                            if (topRatedParks != null && topRatedParks[1].Park_system != "WA State Parks")
                                topRatedParks[1] = park;
                            else
                                topRatedParks[1] = compareParks(topRatedParks[1], park);
                            break;
                        //Compares the national heavyweight champion against possible contenders.
                        case "National Parks":
                            //If the current top national park doesn't actually belong to the national system, switch it with the current park.
                            if (topRatedParks != null && topRatedParks[2].Park_system != "National Parks")
                                topRatedParks[2] = park;
                            else
                                topRatedParks[2] = compareParks(topRatedParks[2], park);
                            break;
                    }
                }
                return topRatedParks;
            }
        }

        //Helper function that compares parks and returns the one with the highest rating.
        //If two parks have the highest rating, the one with the highest vote count wins.
        public ParksModel compareParks(ParksModel topPark, ParksModel currentPark)
        {
            ParksModel newTopPark = topPark;
            //If the current top rated park has no ratings, the current park is considered the top park.
            if (topPark.Ratings == null) { newTopPark = currentPark; }
            //If the current park is null, topPark is unchanged.
            else if (currentPark.Ratings == null) { newTopPark = topPark; }
            //If both parks have the same rating, the park with the higher number of votes is the new top park.
            else if (topPark.Ratings.Average() == currentPark.Ratings.Average())
            {
                //If the count rating of the current park is higher, the current park is the new top park.
                if (topPark.Ratings.Count() < currentPark.Ratings.Count())
                    newTopPark = currentPark;
            }
            //If the current park has a higher average rating than the top park, the current is the new top park.
            else if (topPark.Ratings.Average() < currentPark.Ratings.Average())
            {
                newTopPark = currentPark;
            }
            return newTopPark;
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

        //This function adds a comment to a park defined by the argument ParkId.
        public void AddComment(string ParkId, string[] comment)
        {
            //Gets all parks in the json file
            var Parks = GetParks();

            //If the current park has no current comments, creates a 2d string array and inputs the passed in comment as its first value.
            if (Parks.First(x => x.Id == ParkId).Comments == null)
            {
                //Sets the first value at index 0 in the comments array.
                Parks.First(x => x.Id == ParkId).Comments = new string[][] { comment };
            }
            else
            {
                //Joins the current rating to the existing array of comments.
                var comments = Parks.First(x => x.Id == ParkId).Comments.ToList();
                comments.Add(comment);
                Parks.First(x => x.Id == ParkId).Comments = comments.ToArray();
            }

            //Saves the updated rating to the json file.
            using (var outputStream = File.OpenWrite(JsonFileName))
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