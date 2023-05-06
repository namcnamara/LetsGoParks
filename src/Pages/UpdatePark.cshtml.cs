using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;


namespace LetsGoPark.WebSite.Pages
{
    /// <summary>
    /// Manage the Update of the data for a single record
    /// </summary>

    public class UpdateParkModel : PageModel
    {
        // Data middletier
        public JsonFileParksService ParksService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public UpdateParkModel(JsonFileParksService productService)
        {
            ParksService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ParksModel Park { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Park = ParksService.GetParks().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Park
        /// Call the data layer to Update that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ParksService.UpdateData(Park);

            return RedirectToPage("./Index");
        }
    }
}