using Microsoft.AspNetCore.Mvc;
using blogmanager_nguyenngocvi_22t1020794.Models;
using blogmanager_nguyenngocvi_22t1020794.Data;
using System.Linq; // Thêm thư viện này để dùng hàm FirstOrDefault tìm bài viết

namespace blogmanager_nguyenngocvi_22t1020794.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            ViewData["Title"] = "Danh sách bài viết";
            ViewBag.SoLuong = posts.Count;
            return View(posts);
        }

        // Yêu cầu 3: Tạo Action Details(int id)
        public IActionResult Details(int id)
        {
            // Tìm bài viết trong danh sách có Id trùng với id truyền từ URL vào
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            
            if (post == null)
            {
                return NotFound(); // Trả về trang lỗi 404 nếu không tìm thấy bài viết
            }

            return View(post); // Truyền Model (1 bài viết cụ thể) sang View Details
        }
    }
}