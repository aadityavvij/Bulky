using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(ICategoryRepository db) : Controller
    {
        //private readonly ApplicationDbContext _db = db;
        private readonly ICategoryRepository _categoryRepo = db;

        public IActionResult Index()
        {
            List<Category> objCategoryList = (List<Category>)_categoryRepo.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot be exact same as the Name");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
            if (id == null || id == 0)
            {
                return View(id);
            }
            Category? cat = _categoryRepo.Get(u => u.Id == id);
            if (cat == null)
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
            Category? cat = _categoryRepo.Get(u => u.Id == id);
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

            Category? cat = _categoryRepo.Get(u => u.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            _categoryRepo.Remove(cat);
            _categoryRepo.Save();
            TempData["DeleteMessage"] = "Field Deleted Successfully!";
            return RedirectToAction("Index");
        }

    }
}
