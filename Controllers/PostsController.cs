using Microsoft.AspNetCore.Mvc;
using blogmanager_nguyenngocvi_22t1020794.Models;
using System.Linq; // Thêm thư viện này để dùng hàm FirstOrDefault tìm bài viết

public class PostsController : Controller
{
    // Tạo một hàm riêng chứa danh sách bài viết mẫu để dùng chung cho cả Index và Details
    private List<Post> GetDummyPosts()
    {
        return new List<Post>
        {
            new Post { 
                Id = 1, 
                Title = "C# cơ bản", 
                Author = "Ngọc Vi", 
                ViewCount = 150, 
                PublishedAt = new DateTime(2024, 6, 5), 
                IsPublished = true, 
                Content = "Nội dung bài học C# cơ bản: Tìm hiểu về biến, các kiểu dữ liệu nguyên thủy, vòng lặp (for, while) và cấu trúc rẽ nhánh (if-else, switch-case) trong lập trình..." 
            },
            new Post { 
                Id = 2, 
                Title = "MVC nhập môn", 
                Author = "Quang Huy", 
                ViewCount = 320, 
                PublishedAt = new DateTime(2025, 6, 5), 
                IsPublished = false, 
                Content = "Kiến trúc Model-View-Controller (MVC) trong ASP.NET Core giúp tách biệt rõ ràng giữa logic xử lý dữ liệu (Model), giao diện hiển thị (View) và bộ điều hướng (Controller)..." 
            },
            new Post { 
                Id = 3, 
                Title = "EF Core chuyên sâu", 
                Author = "Ngọc Vi", 
                ViewCount = 580, 
                PublishedAt = new DateTime(2024, 6, 7), 
                IsPublished = true, 
                Content = "Entity Framework Core là một ORM (Object-Relational Mapper) mạnh mẽ của Microsoft, cho phép lập trình viên thao tác trực tiếp với cơ sở dữ liệu thông qua các đối tượng C#..." 
            }
        };
    }

    public IActionResult Index()
    {
        var posts = GetDummyPosts();
        ViewData["Title"] = "Danh sách bài viết";
        ViewBag.SoLuong = posts.Count;
        return View(posts);
    }

    // Yêu cầu 3: Tạo Action Details(int id)
    public IActionResult Details(int id)
    {
        var posts = GetDummyPosts();
        
        // Tìm bài viết trong danh sách có Id trùng với id truyền từ URL vào
        var post = posts.FirstOrDefault(p => p.Id == id);
        
        if (post == null)
        {
            return NotFound(); // Trả về trang lỗi 404 nếu không tìm thấy bài viết
        }

        return View(post); // Truyền Model (1 bài viết cụ thể) sang View Details
    }
}