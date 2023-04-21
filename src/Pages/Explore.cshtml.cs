using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
//Shatakshi
namespace LetsGoPark.WebSite.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly ILogger<ExploreModel> _logger;

        public ExploreModel(ILogger<ExploreModel> logger,
            JsonFileParksService Parkservice)
        {
            _logger = logger;
            Parkservice = Parkservice;
        }
        
       
        public JsonFileParksService Parkservice { get; }
        public IEnumerable<ParksModel> Parks { get; private set; }

        public void OnGet()
        {
            Parks = Parkservice.GetParks();
        }
    }
}
