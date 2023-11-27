using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CatergoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CatergoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        IEnumerable<Catergory> objCatergoryList = _db.Catergories;
        return View(objCatergoryList);
    }

    //GET
    public IActionResult Create()
    {

        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Catergory obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder name cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _db.Catergories.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "Catergory created successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var catergoryFromDb = _db.Catergories.Find(id);

        if (catergoryFromDb == null)
        {
            return NotFound();
        }
        return View(catergoryFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Catergory obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder name cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _db.Catergories.Update(obj);
            _db.SaveChanges();
            TempData["Success"] = "Catergory edited successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var catergoryFromDb = _db.Catergories.Find(id);

        if (catergoryFromDb == null)
        {
            return NotFound();
        }
        return View(catergoryFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Catergories.Find(id);
        if (obj == null)
        {
            NotFound();
        }

        _db.Catergories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Catergory deleted successfully!";
        return RedirectToAction("Index");


    }
}


