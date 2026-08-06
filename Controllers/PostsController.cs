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

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 5; // Số bài viết mỗi trang

            // Bắt đầu từ toàn bộ bài viết
            var query = _context.Posts.AsQueryable();

            // Lọc theo từ khóa tìm kiếm (nếu có)
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || p.Author.Contains(search));
            }

            // Đếm tổng số bài (sau khi lọc)
            int totalPosts = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            // Sắp xếp + Phân trang (Skip/Take)
            var posts = await query
                .OrderByDescending(p => p.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Truyền thông tin phân trang sang View
            ViewData["Title"] = "Danh sách bài viết";
            ViewBag.Search = search;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalPosts = totalPosts;

            return View(posts);
        }

        // Yêu cầu 3: Tạo Action Details(int id)
        public async Task<IActionResult> Details(int id)
        {
            // Dùng AsNoTracking để luôn lấy dữ liệu mới nhất từ database
            var post = await _context.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            
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

            // Lấy bài viết gốc từ database
            var existing = await _context.Posts.FindAsync(id);
            if (existing == null) return NotFound();

            // Ghi đè từng trường (fetch-then-update pattern)
            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.Author = post.Author;
            existing.PublishedAt = post.PublishedAt;
            existing.IsPublished = post.IsPublished;
            existing.ViewCount = post.ViewCount;

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