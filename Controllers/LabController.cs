using blogmanager_nguyenngocvi_22t1020794.Models;
using Microsoft.AspNetCore.Mvc;

public class LabController : Controller
{
    public IActionResult Index()
    {
        // Tạo >= 5 bài viết mẫu có đầy đủ Author và ViewCount
        var baiViet = new List<Post>
        {
            new Post { Id = 1, Title = "C# cơ bản", Author = "Ngọc Vi", ViewCount = 150, IsPublished = true },
            new Post { Id = 2, Title = "MVC nhập môn", Author = "Quang Huy", ViewCount = 320, IsPublished = false },
            new Post { Id = 3, Title = "EF Core chuyên sâu", Author = "Ngọc Vi", ViewCount = 580, IsPublished = true },
            new Post { Id = 4, Title = "LINQ trong ASP.NET", Author = "Minh Tuấn", ViewCount = 450, IsPublished = true },
            new Post { Id = 5, Title = "Xây dựng RESTful API", Author = "Thu Hà", ViewCount = 210, IsPublished = true },
            new Post { Id = 6, Title = "Deploy web lên Cloud", Author = "Ngọc Vi", ViewCount = 95, IsPublished = false }
        };

        // Yêu cầu 1: Bài ĐÃ XUẤT BẢN (IsPublished = true), sắp xếp theo ViewCount GIẢM DẦN
        ViewBag.BaiDaXuatBan = baiViet
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.ViewCount)
            .ToList();

        // Yêu cầu 2: TỔNG số lượt xem của TẤT CẢ bài viết
        ViewBag.TongLuotXem = baiViet.Sum(p => p.ViewCount);

        // Yêu cầu 3: Bài viết có NHIỀU lượt xem NHẤT
        ViewBag.BaiXemNhiềuNhat = baiViet
            .OrderByDescending(p => p.ViewCount)
            .FirstOrDefault();

        return View();
    }
}