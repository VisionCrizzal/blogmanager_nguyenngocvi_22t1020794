using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blogmanager_nguyenngocvi_22t1020794.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndTagRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");

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
    }
}
