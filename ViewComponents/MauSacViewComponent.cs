using CuoiKy.Data;
using CuoiKy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CuoiKy.ViewComponents
{
    public class MauSacViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public MauSacViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<MauSac> mausac = _db.MauSac.ToList();
            return View(mausac);
        }

    }
}
