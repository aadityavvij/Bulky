using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    public class CreateModel(ApplicationDbContext db) : PageModel
    {
		private readonly ApplicationDbContext _db = db;
		public Category cat { get; set; }
		public void OnGet()
        {
			cat = new Category();
        }
        public RedirectToPageResult OnPost(Category cat)
        {
			if (cat.Name == cat.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The display order cannot be exact same as the Name");
			}
			if (ModelState.IsValid)
			{
				_db.Category.Add(cat);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Submission successful";
				return RedirectToPage("/Categories/Index"); // Redirect to the desired action after submission
			}
			return RedirectToPage("/Categories/Index");
		}
    }
}
