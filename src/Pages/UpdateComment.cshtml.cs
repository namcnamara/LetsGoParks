using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LetsGoPark.WebSite.Pages
{
    public class UpdateCommentModel : PageModel
    {
        private readonly ILogger<UpdateCommentModel> _logger;

        public UpdateCommentModel(ILogger<UpdateCommentModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
