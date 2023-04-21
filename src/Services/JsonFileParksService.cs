using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using LetsGoPark.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace LetsGoPark.WebSite.Services
{
   public class JsonFileParksService
    {
        public JsonFileParksService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "parks.json"); }
        }

        public IEnumerable<ParksModel> GetParks()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ParksModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void AddRating(string ParkId, int rating)
        {
            var Parks = GetParks();

            if(Parks.First(x => x.Id == ParkId).Ratings == null)
            {
                Parks.First(x => x.Id == ParkId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = Parks.First(x => x.Id == ParkId).Ratings.ToList();
                ratings.Add(rating);
                Parks.First(x => x.Id == ParkId).Ratings = ratings.ToArray();
            }

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