using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    public class IndexModel(ApplicationDbContext db) : PageModel
    {
		private readonly ApplicationDbContext _db = db;
		public List<Category> objCategoryList { get; set; }
		public void OnGet()
        {
			objCategoryList = _db.Category.ToList();
		}
    }
}
