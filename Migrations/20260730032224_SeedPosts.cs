using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blogmanager_nguyenngocvi_22t1020794.Migrations
{
    /// <inheritdoc />
    public partial class SeedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "IsPublished", "PublishedAt", "Title", "ViewCount" },
                values: new object[,]
                {
                    { 1, "Ngọc Vi", "Nội dung bài học C# cơ bản: Tìm hiểu về biến, các kiểu dữ liệu nguyên thủy, vòng lặp (for, while) và cấu trúc rẽ nhánh (if-else, switch-case) trong lập trình...", true, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# cơ bản", 150 },
                    { 2, "Quang Huy", "Kiến trúc Model-View-Controller (MVC) trong ASP.NET Core giúp tách biệt rõ ràng giữa logic xử lý dữ liệu (Model), giao diện hiển thị (View) và bộ điều hướng (Controller)...", false, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MVC nhập môn", 320 },
                    { 3, "Ngọc Vi", "Entity Framework Core là một ORM (Object-Relational Mapper) mạnh mẽ của Microsoft, cho phép lập trình viên thao tác trực tiếp với cơ sở dữ liệu thông qua các đối tượng C#...", true, new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core chuyên sâu", 580 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
