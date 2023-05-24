using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class UpdateCommentModel : PageModel
    {
        //Adds logger to class
        private readonly ILogger<UpdateCommentModel> _logger;

        //Class constructor
        public UpdateCommentModel(ILogger<UpdateCommentModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sends to the logger the UpdateComment logger message
        /// </summary>
        public void OnGet()
        {
            //Logs successful request
            _logger.LogInformation("UpdateComment.cshtml Requested");
        }
    }
}
