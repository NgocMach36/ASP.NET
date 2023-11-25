using CuoiKy.Data;
using CuoiKy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CuoiKy.ViewComponents
{
    public class GioHangViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public GioHangViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<GioHang> giohang = _db.GioHang.Include("SanPham").ToList();
            ViewBag.GioHang = giohang;             

            return View();
        }

    }
}
