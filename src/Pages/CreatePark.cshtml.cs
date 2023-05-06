
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    public class CreateModel : PageModel
    {
        // Data middle tier
        public JsonFileParksService ParksService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public CreateModel(JsonFileParksService productService)
        {
            ParksService = productService;
        }

        // The data to show
        public ParksModel Park;

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet()
        {
            Park = ParksService.CreateData();

            return RedirectToPage("./UpdatePark", new { Id = Park.Id });
        }
    }
}