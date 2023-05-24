using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class DeleteCommentModel : PageModel
    {
        //Adds logger to class
        private readonly ILogger<DeleteCommentModel> _logger;

        //Contructor for DeleteCommentModel
        public DeleteCommentModel(ILogger<DeleteCommentModel> logger)
        {
            //Assigns logger to class
            _logger = logger;
        }

        /// <summary>
        /// Calls logger and submits the action of OnGet message to it
        /// </summary>
        public void OnGet()
        {
            _logger.LogInformation("Request DeleteComment.cshtml");
        }
    }
}
