using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class IndexModel : PageModel 
    {
        //Adds logger to class
        private readonly ILogger<IndexModel> _logger;

        //Class constructor
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileParksService parkService)
        {
            //Assigns logger to class
            _logger = logger;
            //Assigns instance of JsonFileParksService for class to use
            ParkService = parkService;
        }
        
       //Instance of JsonFileparksService
        public JsonFileParksService ParkService { get; }
        //Enumerable of all parks in parks.json
        public IEnumerable<ParksModel> Parks { get; private set; }

        public void OnGet()
        {
            //finds all parks in parks.json
            Parks = ParkService.GetParks();
        }
    }
}
