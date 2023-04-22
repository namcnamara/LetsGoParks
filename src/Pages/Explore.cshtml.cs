using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly ILogger<ExploreModel> _logger;

        public ExploreModel(ILogger<ExploreModel> logger,
            JsonFileParksService parkService)
        {
            _logger = logger;
            ParkService = parkService;
        }


        public JsonFileParksService ParkService { get; }
        public IEnumerable<ParksModel> Parks { get; private set; }

        public void OnGet()
        {
            Parks = ParkService.GetParks();
        }
    }
}
