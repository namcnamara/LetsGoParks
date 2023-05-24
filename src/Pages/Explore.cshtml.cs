using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class ExploreModel : PageModel
    {
        //Adds logger to class
        private readonly ILogger<ExploreModel> _logger;

        //Constructor for class
        public ExploreModel(ILogger<ExploreModel> logger,
            JsonFileParksService parkService)
        {
            //assigns logger to class
            _logger = logger;
            //assigns instance of ParkService to the class so it can access parks.json
            ParkService = parkService;
        }

        //Instance of JsonfileParksService
        public JsonFileParksService ParkService { get; }
        //Enumerable of all parks in parks.json
        public IEnumerable<ParksModel> Parks { get; private set; }

        /// <summary>
        /// Populates the ParksService with park objects from parks.json
        /// </summary>
        public void OnGet()
        {
            //Gets all parks upon start
            Parks = ParkService.GetParks();
        }
    }
}
