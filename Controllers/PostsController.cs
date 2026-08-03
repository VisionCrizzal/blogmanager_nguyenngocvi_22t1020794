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
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id) return NotFound();
            if (!ModelState.IsValid) return View(post);

            _context.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null) 
            { 
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync(); 
            }
            return RedirectToAction(nameof(Index));
        }
    }
}