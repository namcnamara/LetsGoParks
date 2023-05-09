using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LetsGoPark.WebSite.Pages
{
    public class ReadParkModel : PageModel
    {
        // Data middletier
        public JsonFileParksService ParksService { get; }
        public ReadParkModel(JsonFileParksService parkService)
        { 
            ParksService = parkService;
        }
        public ParksModel Park;
        public void OnGet(string id)
        {
            Park = ParksService.GetParks().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}