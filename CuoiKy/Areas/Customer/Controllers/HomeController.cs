using CuoiKy.Data;
using CuoiKy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CuoiKy.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();
            return View(sanpham);


        }
        [HttpPost]
        [Authorize]
        public IActionResult Index(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value;

            var giohangFromDb = _db.GioHang.FirstOrDefault(gh => gh.SanPhamId == giohang.SanPhamId);
            if (giohangFromDb == null)
            {
                _db.GioHang.Add(giohang);
            }
            else
            {
                giohangFromDb.Quantity += giohang.Quantity;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }
        
        public IActionResult Page404()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View();
        }

        [HttpGet]        
        
        public IActionResult Checkout()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang
                .Include("SanPham")
                .Where(gh => gh.ApplicationUserId == claim.Value)
                .ToList(),
                HoaDon = new HoaDon()
            };
            giohang.HoaDon.ApplicationUser = _db.ApplicationUser.FirstOrDefault(user => user.Id == claim.Value);
            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.Name;
            giohang.HoaDon.Address = giohang.HoaDon.ApplicationUser.Address;
            giohang.HoaDon.PhoneNumber = giohang.HoaDon.ApplicationUser.PhoneNumber;

            foreach (var item in giohang.DsGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += item.ProductPrice;
            }
            return View(giohang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(GioHangViewModel giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.DsGioHang = _db.GioHang.Include("SanPham")
                .Where(gh => gh.ApplicationUserId == claim.Value).ToList();
            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận";
            foreach (var item in giohang.DsGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += item.ProductPrice;
            }
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();
            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity
                };
                _db.ChiTietHoaDon.Add(chitiethoadon);
                _db.SaveChanges();
            }
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();
            return RedirectToAction("Confirm");
        }

        public IActionResult Single(int sanphamId)
        {

            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamId,
                SanPham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == sanphamId),
                Quantity = 1
            };
            return View(giohang);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Single(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            Console.WriteLine(identity);
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value;

            var giohangFromDb = _db.GioHang.FirstOrDefault(gh => gh.SanPhamId == giohang.SanPhamId);
            if (giohangFromDb == null)
            {
                _db.GioHang.Add(giohang);
            }
            else
            {
                giohangFromDb.Quantity += giohang.Quantity;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Shop()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();
            return View(sanpham);
        }
        public IActionResult ShopKhac()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();
            return View(sanpham);
        }

        public IActionResult ShopKhac1()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();
            return View(sanpham);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var giohang = _db.GioHang.FirstOrDefault(giohang => giohang.Id == id);
            if (giohang == null)
            {
                return NotFound();
            }
            else
            {
                _db.GioHang.Remove(giohang);
                _db.SaveChanges();
                return Json(new { success = true });

            }
        }

        public IActionResult Giam(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity -= 1;
            if(giohang.Quantity == 0)
			{
                _db.GioHang.Remove(giohang);
			}
            _db.SaveChanges();
            return RedirectToAction("Checkout");
        }
        public IActionResult Tang(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity += 1;
            _db.SaveChanges();
            return RedirectToAction("Checkout");
        }
        public IActionResult Xoa(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            _db.GioHang.Remove(giohang);
            _db.SaveChanges();
            return RedirectToAction("Checkout");
        }
        public IActionResult XoaGioHang(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            _db.GioHang.Remove(giohang);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Confirm()
        {
            return View();
        }

    }
}