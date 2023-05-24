using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class AboutUsModel : PageModel
    {
        //Adds a logger to the class
        private readonly ILogger<AboutUsModel> _logger;

        //AboutUs constructor
        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            //Assigns logger variable
            _logger = logger;
        }

       
    }
}
