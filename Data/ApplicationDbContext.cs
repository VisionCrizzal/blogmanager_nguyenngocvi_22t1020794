using Microsoft.EntityFrameworkCore;
using blogmanager_nguyenngocvi_22t1020794.Models;

namespace blogmanager_nguyenngocvi_22t1020794.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                Title = "C# cơ bản",
                Author = "Ngọc Vi",
                ViewCount = 150,
                PublishedAt = new DateTime(2024, 6, 5),
                IsPublished = true,
                Content = "Nội dung bài học C# cơ bản: Tìm hiểu về biến, các kiểu dữ liệu nguyên thủy, vòng lặp (for, while) và cấu trúc rẽ nhánh (if-else, switch-case) trong lập trình..."
            },
            new Post
            {
                Id = 2,
                Title = "MVC nhập môn",
                Author = "Quang Huy",
                ViewCount = 320,
                PublishedAt = new DateTime(2025, 6, 5),
                IsPublished = false,
                Content = "Kiến trúc Model-View-Controller (MVC) trong ASP.NET Core giúp tách biệt rõ ràng giữa logic xử lý dữ liệu (Model), giao diện hiển thị (View) và bộ điều hướng (Controller)..."
            },
            new Post
            {
                Id = 3,
                Title = "EF Core chuyên sâu",
                Author = "Ngọc Vi",
                ViewCount = 580,
                PublishedAt = new DateTime(2024, 6, 7),
                IsPublished = true,
                Content = "Entity Framework Core là một ORM (Object-Relational Mapper) mạnh mẽ của Microsoft, cho phép lập trình viên thao tác trực tiếp với cơ sở dữ liệu thông qua các đối tượng C#..."
            }
        );
    }
}
