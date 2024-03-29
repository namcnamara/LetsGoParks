using System;
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

        /// <summary>
        /// Function returns a IEnumerable container of all parks found within the json file. 
        /// </summary>
        /// <returns>Returns an enumerable of parks objects</returns>
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

        /// <summary>
        /// Function returns only the Highest Rated Park found within the json file.
        //If two parks have the same rating, it returns the park with the highest vote count.
        /// </summary>
        /// <returns>Returns highest rated park</returns>
        public ParksModel GetHighestRatedPark()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                //Parks is a list of all parks objects in parks.json
                IEnumerable<ParksModel> Parks = JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                //Grabs the first park in the list and sets it to the top park
                ParksModel topPark = Parks.FirstOrDefault();
                //Uses the CompareParks() function to select the highest rated park from parks.json
                foreach (var park in Parks)
                {
                    //Compares rating for each park, and swaps them if necessary
                    topPark = CompareParks(topPark, park);
                }
                return topPark;
            }
        }

        /// <summary>
        /// This function compares all park objects in the parks.json database. It saves the highest parks in each of the following 3 catagories:
        /// National Parks, WA State Parks, City Parks
        ///
        /// </summary>
        /// <returns>This function returns an array of size 3 that holds the top city, state, and national park by rating. </returns>
        public ParksModel[] GetHighestRatedParks()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                //Parks is a list of all parks objects in parks.json
                IEnumerable<ParksModel> Parks = JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                //topRatedParks is an array that holds the 3 highest rated parks for each catagory.
                //Index 0 is for city parks, index 1 is for state parks, and index 2 is for national parks.
                ParksModel[] topRatedParks = { Parks.FirstOrDefault(x => x.Park_system == ParkSystemEnum.National), 
                    Parks.FirstOrDefault(x => x.Park_system == ParkSystemEnum.National), 
                    Parks.FirstOrDefault(x => x.Park_system == ParkSystemEnum.State)};
                foreach (var park in Parks)
                {
                    switch(park.Park_system)
                    {
                        //Compares top city park against potential contenders.
                        case ParkSystemEnum.City:
                            //If the current top city park doesn't actually belong to the city system, switch it with the current park.
                            if (topRatedParks[0].Park_system != ParkSystemEnum.City)
                                topRatedParks[0] = park;
                            else
                                topRatedParks[0] = CompareParks(topRatedParks[0], park);
                            break;
                        //Compares top state park against potential contenders.
                        case ParkSystemEnum.State:
                            //If the current top state park doesn't actually belong to the state system, switch it with the current park.
                            if (topRatedParks[1].Park_system != ParkSystemEnum.State)
                                topRatedParks[1] = park;
                            else
                                topRatedParks[1] = CompareParks(topRatedParks[1], park);
                            break;
                        //Compares the national heavyweight champion against possible contenders.
                        case ParkSystemEnum.National:
                            //If the current top national park doesn't actually belong to the national system, switch it with the current park.
                            if (topRatedParks[2].Park_system != ParkSystemEnum.National)
                                topRatedParks[2] = park;
                            else
                                topRatedParks[2] = CompareParks(topRatedParks[2], park);
                            break;
                    }
                }
                return topRatedParks;
            }
        }

        /// <summary>
        /// Helper function that compares parks and returns the one with the highest rating.
        //If two parks have the highest rating, the one with the highest vote count wins.
        /// </summary>
        /// <param name="topPark"></param>
        /// <param name="currentPark"></param>
        /// <returns>Returns the park with a higher rating</returns>
        public ParksModel CompareParks(ParksModel topPark, ParksModel currentPark)
        {
            //Sets the returning park to toPark parameter
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


        /// <summary>
        /// This function adds a rating to a park defined by the argument ParkId.
        /// </summary>
        /// <param name="parkId"></param>
        /// <param name="rating"></param>
        /// <returns>Returns true if successfully added a rating</returns>
        public bool AddRating(string parkId, int rating)
        {
            // If the ParkID is invalid, return
            if (string.IsNullOrEmpty(parkId))
            {
                return false;
            }
            //parks is a list of all parks objects in parks.json
            var parks = GetParks();

            // Look up the park, if it does not exist, return
            var data = parks.FirstOrDefault(x => x.Id.Equals(parkId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(parks);

            return true;
        }

        /// <summary>
        /// This function saves all updated data to parks.json by creating a new json from the parks enumerable object
        /// </summary>
        /// <param name="parks"></param>
        private void SaveData(IEnumerable<ParksModel> parks)
        {
            
            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ParksModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true,
                    }),
                    parks
                );
            }
        }

        /// <summary>
        /// This function adds a comment to a park defined by the argument ParkId.
        /// </summary>
        /// <param name="ParkId"></param>
        /// <param name="comment"></param>
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
            SaveData(Parks);
        }

        /// <summary>
        /// Updates a current comment for the selected ParkId.
        /// </summary>
        /// <param name="selectedParkId"></param>
        /// <param name="comment"></param>
        /// <param name="oldCommentIndex"></param>
        /// <returns>If a comment is unable to update, returns false, otherwise return true</returns>
        public bool UpdateComment(string selectedParkId, string[] comment, int oldCommentIndex)
        {
            if(comment == null || comment.Length == 0)
                { return false; }
            //Gets all parks in the json file
            var Parks = GetParks();
            if (selectedParkId == null)
                { return false; }
            selectedParkId = selectedParkId.Trim();
            //Joins the current rating to the existing array of comments.
            var park = Parks.FirstOrDefault(x => x.Id == selectedParkId);
            if (park == null)
                { return false; }
            var comments = Parks.First(x => x.Id == selectedParkId).Comments.ToList();
            int numComments = comments.Count;
            if (oldCommentIndex > numComments-1)
                { return false; }
            else
            {
                comments[oldCommentIndex] = comment;
                Parks.First(x => x.Id == selectedParkId).Comments = comments.ToArray();

                //Saves the updated rating to the json file.
                SaveData(Parks);
                return true;
            }
            
        }
        /// <summary>
        /// This function removes a comment from a specified park, chosen by ParkId
        /// </summary>
        /// <param name="selectedParkId"></param>
        /// <param name="commentIndex"></param>
        /// <returns>Return true if comment was successfully deleted, false otherwise</returns>
        public bool DeleteComment(string selectedParkId, int commentIndex)
        {
            if (selectedParkId == null)
            { return false; }
            selectedParkId = selectedParkId.Trim();
            //Gets all parks in the json file
            var Parks = GetParks();
            if (commentIndex < 0) { return false; }


            //Deletes the desired comment if park is found
            var park = Parks.FirstOrDefault(x => x.Id == selectedParkId);

            if (park == null) { return false; }
            if (Parks.First(x => x.Id == selectedParkId).Comments == null) { return false; }    
            var comments = Parks.First(x => x.Id == selectedParkId).Comments.ToList();
            //If you remove the only comment, set comments to null
            if (comments.Count == 1)
            {
                Parks.First(x => x.Id == selectedParkId).Comments = null;
            }
            else
            {
                //Deletes the comment from the list
                comments.RemoveAt(commentIndex);
                Parks.First(x => x.Id == selectedParkId).Comments = comments.ToArray();
            }

            //Saves the updated rating to the json file.
            SaveData(Parks);
            return true;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public ParksModel UpdateData(ParksModel data)
        {
            var parks = GetParks();
            var parkData = parks.FirstOrDefault(x => x.Id.Equals(data.Id));
            if (parkData == null)
            {
                return null;
            }

            //Copy over variables
            parkData.Id = data.Id;
            parkData.Description = data.Description;
            parkData.Url = data.Url;
            parkData.Image = data.Image;
            parkData.Address = data.Address;
            parkData.Park_system = data.Park_system;
            parkData.Activities = data.Activities;
            parkData.Map_brochure = data.Map_brochure;
            parkData.Permits = data.Permits;
            
            SaveData(parks);

            return parkData;
        }


        /// <summary>
        /// Create a new park using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns></returns>
        public ParksModel CreateData(ParksModel parkIn)
        {
            //New park passed in
            var newPark = parkIn;
           
            // data isn't filled, return
            if (parkIn.Id == "" || parkIn.Id == null)
            {
                return null;
            }
            // Get the current set, and append the new record to it
            var dataSet = GetParks();
            dataSet = dataSet.Append(newPark);

            SaveData(dataSet);

            return newPark;
        }


        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ParksModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetParks();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            //If The data isn't in the dataset, return null
            if (data == null) { return null; }
            var newDataSet = GetParks().Where(m => m.Id.Equals(id) == false);

            SaveData(newDataSet);

            return data;
        }
    }
}