
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class CreateParkModel : PageModel
    {
        // Data middle tier
        public JsonFileParksService ParksService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="parkService"></param>
        public CreateParkModel(JsonFileParksService parkService)
        {
            ParksService = parkService;
            Park = new ParksModel()
            {
                Id = "",
                Url = "",
                Image = "",
                Description = "",
                Ratings = null,
                Address = "",
                Phone = "",
                Park_system = "",
                Activities = "",
                Map_brochure = "",
                Permits = "",
                Comments = null,
            };
        }

        // The data to show
        public ParksModel Park { get; set; }

        //Method taken upon submission of post button
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Calls the CreateData function in JsonFileParksService.cs
            ParksService.CreateData(Park);

            //Redirects to index upon completion of created park
            return RedirectToPage("./Index");
        }
    }
}