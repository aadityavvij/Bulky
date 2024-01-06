using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
	public class editModel(ApplicationDbContext db) : PageModel
	{
		private readonly ApplicationDbContext _db = db;

		[BindProperty]
		public Category cat { get; set; }

		public void OnGet(int id)
		{
			cat = _db.Category.Find(id);
		}
		public RedirectToPageResult OnPost(Category cat)
		{
			if (cat.Name == cat.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The display order cannot be exact same as the Name");
			}
			if (ModelState.IsValid)
			{
				_db.Category.Update(cat);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Submission successful";
				return RedirectToPage("/Categories/Index"); // Redirect to the desired action after submission
			}
			return RedirectToPage("/Categories/Index");
		}
	}

}
