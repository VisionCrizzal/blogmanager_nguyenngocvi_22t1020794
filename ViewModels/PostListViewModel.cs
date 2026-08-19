using blogmanager_nguyenngocvi_22t1020794.Models;

namespace blogmanager_nguyenngocvi_22t1020794.ViewModels
{
    public class PostListViewModel
    {
        public List<Post> Posts { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        
        // Thêm các thuộc tính phục vụ lọc theo danh mục và thẻ
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }
        public int TotalPosts { get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
