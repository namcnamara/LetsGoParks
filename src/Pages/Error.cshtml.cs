using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
// random comment ;)
namespace LetsGoPark.WebSite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        //Id of the request
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //Adds a logger to the class
        private readonly ILogger<ErrorModel> _logger;

        //Class constructor
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            //Assigns logger to class instance
            _logger = logger;
        }

        /// <summary>
        /// Gets the requestId of the current http request
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
