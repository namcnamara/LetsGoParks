using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;

namespace LetsGoPark.WebSite.Pages
{
    /// <summary>
    /// Manage the Delete of the data for a single record
    /// </summary>
    public class DeleteParkModel : PageModel
    {
        // Data middletier
        public JsonFileParksService ParksService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="parkService"></param>
        public DeleteParkModel(JsonFileParksService parkService)
        {
            ParksService = parkService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ParksModel Park { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public bool OnGet(string id)
        {
            Park = ParksService.GetParks().FirstOrDefault(m => m.Id.Equals(id));
            if (Park == null) { return false; }
            return true;
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Park
        /// Call the data layer to Delete that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            //calls DeleteData method in JsonFileParksService
            ParksService.DeleteData(Park.Id);
            //Redirect to the home page
            return RedirectToPage("./Index");
        }
    }
}