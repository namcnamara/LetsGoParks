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
        public ParksController(JsonFileParksService Parkservice)
        {
            Parkservice = Parkservice;
        }

        public JsonFileParksService Parkservice { get; }

        [HttpGet]
        public IEnumerable<ParksModel> Get()
        {
            return Parkservice.GetParks();
        }

        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            Parkservice.AddRating(request.ParkId, request.Rating);
            
            return Ok();
        }

        public class RatingRequest
        {
            public string ParkId { get; set; }
            public int Rating { get; set; }
        }
    }
}