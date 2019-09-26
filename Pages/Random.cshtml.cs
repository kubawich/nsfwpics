using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RandomModel'
    public class RandomModel : PageModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RandomModel'
    {
        [BindProperty]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RandomModel.Image'
        public NSFWpics.Models.Image Image { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RandomModel.Image'

        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RandomModel.OnGet()'
        public IActionResult OnGet()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RandomModel.OnGet()'
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