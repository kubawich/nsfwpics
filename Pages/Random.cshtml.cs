using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class RandomModel : PageModel
    {
        [BindProperty]
        public NSFWpics.Models.Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet()
        {
			try
			{
				Image = new NSFWpics.Models.Random().GetEntry(null, Image);            
				return Page();
			}
			catch (System.Exception)
			{
				return Redirect("https://nsfwpics.pw/Random");
			}
        }
    }
}