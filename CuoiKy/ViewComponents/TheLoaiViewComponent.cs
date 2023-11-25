using CuoiKy.Data;
using CuoiKy.Models;
using Microsoft.AspNetCore.Mvc;

namespace CuoiKy.ViewComponents
{
    public class TheLoaiViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<TheLoai> theloai = _db.TheLoai.ToList(); 
            return View(theloai);
        }

    }
}
