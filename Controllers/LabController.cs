using blogmanager_nguyenngocvi_22t1020794.Models;
using Microsoft.AspNetCore.Mvc;

public class LabController : Controller
{
    public IActionResult Index()
    {
        var baiViet = new List<Post>
        {
            new Post { Id = 1, Title = "C# cơ bản", Author = "Ngọc Vĩ", ViewCount = 150, IsPublished = true },
            new Post { Id = 2, Title = "MVC nhập môn", Author = "Quang Huy", ViewCount = 320, IsPublished = false },
            new Post { Id = 3, Title = "EF Core chuyên sâu", Author = "Ngọc Vĩ", ViewCount = 580, IsPublished = true },
            new Post { Id = 4, Title = "LINQ trong ASP.NET", Author = "Minh Tuấn", ViewCount = 450, IsPublished = true },
            new Post { Id = 5, Title = "Xây dựng RESTful API", Author = "Thu Hà", ViewCount = 210, IsPublished = true },
            new Post { Id = 6, Title = "Deploy web lên Cloud", Author = "Ngọc Vi", ViewCount = 85, IsPublished = true } // Bài này < 100 view để ra nhãn "Thường"
        };

        ViewBag.BaiDaXuatBan = baiViet.Where(p => p.IsPublished).OrderByDescending(p => p.ViewCount).ToList();
        ViewBag.TongLuotXem = baiViet.Sum(p => p.ViewCount);
        ViewBag.BaiXemNhiềuNhat = baiViet.OrderByDescending(p => p.ViewCount).FirstOrDefault();

        return View();
    }
}