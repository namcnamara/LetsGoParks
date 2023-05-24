using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class PrivacyModel : PageModel
    {
        //Adds logger to class
        private readonly ILogger<PrivacyModel> _logger;

        //Constructor class
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            //Assign logger to class
            _logger = logger;
        }

        /// <summary>
        /// Called upon page rendering to log 
        /// </summary>
        public void OnGet()
        {
            //logs successful loading of page to logger
            _logger.LogInformation("Request Privacy.cshtml");
        }
    }
}
