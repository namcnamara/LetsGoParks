using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class ContactModel : PageModel
    {
        //Adds logger to the class
        private readonly ILogger<ContactModel> _logger;

        //Constructor for ContactModel
        public ContactModel(ILogger<ContactModel> logger)
        {
            //Assigns logger to the class
            _logger = logger;
        }


    }
}
