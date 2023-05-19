using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParksController : ControllerBase
    {
        /// <summary>
        /// Creates an instance of ParksController using a JsonFileParksService object
        /// </summary>
        /// <param name="parkService"></param>
        public ParksController(JsonFileParksService parkService)
        {
            ParkService = parkService;
        }

        //ParkService to manipulate Json database parks.json
        public JsonFileParksService ParkService { get; }

        //Sends a get request to get all parks in the parks.json database
        [HttpGet]
        public IEnumerable<ParksModel> Get()
        {
            return ParkService.GetParks();
        }

        /// <summary>
        /// Adds a patch to update rating
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ParkService.AddRating(request.ParkId, request.Rating);
            
            return Ok();
        }

        /// <summary>
        /// Manipulates and retrieves rating information for a given park
        /// </summary>
        public class RatingRequest
        {
            //Id of park to get the rating of
            public string ParkId { get; set; }
            //Rating of park selected
            public int Rating { get; set; }
        }
    }
}