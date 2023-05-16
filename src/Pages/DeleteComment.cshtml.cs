using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class DeleteCommentModel : PageModel
    {
        private readonly ILogger<DeleteCommentModel> _logger;

        public DeleteCommentModel(ILogger<DeleteCommentModel> logger)
        {
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
