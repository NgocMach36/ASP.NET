using CuoiKy.Data;
using CuoiKy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CuoiKy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MauSacController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MauSacController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var mausac = _db.MauSac.ToList();
            ViewBag.Mausac = mausac;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MauSac mausac)
        {
            if (ModelState.IsValid)
            {
                _db.MauSac.Add(mausac);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var mausac = _db.MauSac.Find(id);
            return View(mausac);
        }

        [HttpPost]
        public IActionResult Edit(MauSac mausac)
        {
            if (ModelState.IsValid)
            {
                _db.MauSac.Update(mausac);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var mausac = _db.MauSac.Find(id);
            return View(mausac);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var mausac = _db.MauSac.Find(id);
            if (mausac == null)
            {
                return NotFound();
            }

            _db.MauSac.Remove(mausac);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var mausac = _db.MauSac.Find(id);
            ViewBag.MauSac = mausac;
            return View(mausac);
        }
    }
}
