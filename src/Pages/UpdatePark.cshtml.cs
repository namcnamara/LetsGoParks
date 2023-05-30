using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using LetsGoPark.WebSite.Models;
using LetsGoPark.WebSite.Services;
using System.Xml.Linq;


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
        /// <param name="parkService"></param>
        public UpdateParkModel(JsonFileParksService parkService)
        {
            ParksService = parkService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ParksModel Park { get; set; }
        //New Id to be updated
        [BindProperty]
        public string Name { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            Park = ParksService.GetParks().FirstOrDefault(m => m.Id.Equals(id));
            Name = Park.Id;
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
            //If the model is in an inactive state
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //If the ID hasn't been changed:
            if(Name == Park.Id)
            {
                ParksService.UpdateData(Park);
            }
            //If the ID has been changed delete old park and create new park
            else
            {
                ParksService.DeleteData(Park.Id);
                Park.Id = Name;
                ParksService.CreateData(Park);
            }
            //Redirect to Index page
            return RedirectToPage("./Index");
        }
    }
}