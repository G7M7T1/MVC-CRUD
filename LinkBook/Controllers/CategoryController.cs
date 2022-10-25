using LinkBook.DataAccess;
using LinkBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkBook.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }

    //get
    public IActionResult Create()
    {
        return View();
    }
    
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "Name Cannot Match The Display Order");
        }
        
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        var categoryFromDb = _db.Categories.Find(id);
        // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
        // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }
    
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "Name Cannot Match The Display Order");
        }
        
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    
    // delete get
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        var categoryFromDb = _db.Categories.Find(id);
        // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
        // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        
        _db.Categories.Remove(categoryFromDb);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    //post
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Delete(int? id)
    // {
    //     var obj = _db.Categories.Find(id);
    //     if (obj == null)
    //     {
    //         return NotFound();
    //     }
    //     _db.Categories.Remove(obj);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    // }
}