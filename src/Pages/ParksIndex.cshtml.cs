using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class ParksIndexModel : PageModel
    {
        private readonly ILogger<ParksIndexModel> _logger;

        public ParksIndexModel(ILogger<ParksIndexModel> logger,
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
