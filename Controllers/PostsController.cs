using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using blogmanager_nguyenngocvi_22t1020794.Models;
using blogmanager_nguyenngocvi_22t1020794.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace blogmanager_nguyenngocvi_22t1020794.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? search, int? categoryId, int? tagId, string? sort, int page = 1)
        {
            int pageSize = 5;

            // Include để nạp dữ liệu liên kết (Eager Loading)
            var query = _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .AsQueryable();

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || p.Author.Contains(search));
            }

            // Lọc theo danh mục (dropdown)
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Lọc theo thẻ
            if (tagId.HasValue && tagId > 0)
            {
                query = query.Where(p => p.Tags.Any(t => t.Id == tagId));
            }

            // Sắp xếp bằng LINQ (switch)
            query = sort switch
            {
                "title" => query.OrderBy(p => p.Title),
                "oldest" => query.OrderBy(p => p.PublishedAt),
                _ => query.OrderByDescending(p => p.PublishedAt)
            };

            int totalPosts = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

            var posts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Sử dụng ViewModel thay cho ViewBag
            var viewModel = new blogmanager_nguyenngocvi_22t1020794.ViewModels.PostListViewModel
            {
                Posts = posts,
                CurrentPage = page,
                TotalPages = totalPages,
                Search = search,
                Sort = sort,
                CategoryId = categoryId,
                TagId = tagId,
                TotalPosts = totalPosts,
                Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync(),
                Tags = await _context.Tags.OrderBy(t => t.Name).ToListAsync()
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (post == null) 
                return NotFound();

            return View(post); 
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(
                await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
            ViewBag.AllTags = await _context.Tags.OrderBy(t => t.Name).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, int[] selectedTags, string? newTags)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
                ViewBag.AllTags = await _context.Tags.OrderBy(t => t.Name).ToListAsync();
                return View(post);
            }

            // Gắn Tags đã chọn
            if (selectedTags != null && selectedTags.Length > 0)
            {
                post.Tags = await _context.Tags.Where(t => selectedTags.Contains(t.Id)).ToListAsync();
            }

            // Tạo Tags mới (nếu có)
            if (!string.IsNullOrWhiteSpace(newTags))
            {
                var tagNames = newTags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var tagName in tagNames)
                {
                    var existing = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
                    if (existing != null)
                    {
                        post.Tags.Add(existing);
                    }
                    else
                    {
                        var newTag = new Tag { Name = tagName };
                        _context.Tags.Add(newTag);
                        post.Tags.Add(newTag);
                    }
                }
            }

            // Gán OwnerId = user hiện tại
            post.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return NotFound();

            // Kiểm tra quyền: chỉ chủ sở hữu hoặc Admin mới được sửa
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (post.OwnerId != null && post.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();

            ViewBag.Categories = new SelectList(
                await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name", post.CategoryId);
            ViewBag.AllTags = await _context.Tags.OrderBy(t => t.Name).ToListAsync();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post, int[] selectedTags, string? newTags)
        {
            if (id != post.Id) return NotFound();

            var existing = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return NotFound();

            // Kiểm tra quyền: chỉ chủ sở hữu hoặc Admin mới được sửa
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existing.OwnerId != null && existing.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();

            // Ghi đè từng trường
            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.Author = post.Author;
            existing.PublishedAt = post.PublishedAt;
            existing.IsPublished = post.IsPublished;
            existing.ViewCount = post.ViewCount;
            existing.CategoryId = post.CategoryId;

            // Cập nhật Tags: xóa hết rồi gắn lại
            existing.Tags.Clear();
            if (selectedTags != null && selectedTags.Length > 0)
            {
                var tags = await _context.Tags.Where(t => selectedTags.Contains(t.Id)).ToListAsync();
                foreach (var tag in tags) existing.Tags.Add(tag);
            }

            // Tạo Tags mới
            if (!string.IsNullOrWhiteSpace(newTags))
            {
                var tagNames = newTags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var tagName in tagNames)
                {
                    var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
                    if (existingTag != null)
                        existing.Tags.Add(existingTag);
                    else
                    {
                        var newTag = new Tag { Name = tagName };
                        _context.Tags.Add(newTag);
                        existing.Tags.Add(newTag);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return NotFound();

            // Admin hoặc chủ sở hữu mới được xóa
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (post.OwnerId != null && post.OwnerId != userId && !User.IsInRole("Admin"))
                return Forbid();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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