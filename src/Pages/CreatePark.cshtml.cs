
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
        /// <param name="productService"></param>
        public CreateParkModel(JsonFileParksService productService)
        {
            ParksService = productService;
        }

        // The data to show
        [BindProperty]
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