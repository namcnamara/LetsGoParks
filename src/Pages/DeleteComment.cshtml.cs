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

        public void OnGet()
        {
            _logger.LogInformation("Request DeleteComment.cshtml");
        }
    }
}
