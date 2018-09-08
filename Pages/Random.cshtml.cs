using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NSFWpics.Pages
{
    public class RandomModel : PageModel
    {
        [BindProperty]
        public Image Image { get; set; }

        [HttpGet]
        public IActionResult OnGet()
        {
			Image = DBEntities.DBEntity.Instance.Random(Image);
            
            return Page();
        }
    }
}