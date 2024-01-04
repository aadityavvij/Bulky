using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Category.ToList();
            return View(objCategoryList);
        }
		public IActionResult Create()
		{
			return View(new Category());
		}
		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if(obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The display order cannot be exact same as the Name");
			}
			if (ModelState.IsValid)
			{
				_db.Category.Add(obj);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Submission successful";
				return RedirectToAction("Index"); // Redirect to the desired action after submission
			}
			else
			{
				return View(obj);
			}
		}
	}
}
