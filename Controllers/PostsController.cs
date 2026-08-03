using Microsoft.AspNetCore.Mvc;
using blogmanager_nguyenngocvi_22t1020794.Models;
using blogmanager_nguyenngocvi_22t1020794.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace blogmanager_nguyenngocvi_22t1020794.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync();
            
            ViewData["Title"] = "Danh sách bài viết";
            ViewBag.SoLuong = posts.Count;
            return View(posts);
        }

        // Yêu cầu 3: Tạo Action Details(int id)
        public async Task<IActionResult> Details(int id)
        {
            // Tìm bài viết theo khóa chính
            var post = await _context.Posts.FindAsync(id);
            
            if (post == null) 
                return NotFound();

            return View(post); 
        }
    }
}