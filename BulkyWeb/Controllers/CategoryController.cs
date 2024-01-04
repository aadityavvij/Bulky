using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult Edit(int? id)
		{
			if(id==null || id == 0)
			{
				return View(id);
			}
			Category? cat = _db.Category.Find(id);
			if(cat == null)
			{
				return View(id);
			}
			return View(cat);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "The display order cannot be exact same as the Name");
			}
			if (ModelState.IsValid)
			{
				_db.Category.Update(obj);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Field Edited Successfully!";
				return RedirectToAction("Index");
			}
			else
			{
				return View(obj);
			}
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return View(id);
			}
			Category? cat = _db.Category.Find(id);
			if (cat == null)
			{
				return View(id);
			}
			return View(cat);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? cat = _db.Category.Find(id);

			if (cat == null)
			{
				return NotFound();
			}

			_db.Category.Remove(cat);
			_db.SaveChanges();
			TempData["DeleteMessage"] = "Field Deleted Successfully!";
			return RedirectToAction("Index");
		}

	}
}
