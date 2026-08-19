using System.ComponentModel.DataAnnotations;

namespace blogmanager_nguyenngocvi_22t1020794.Models;

public class Post
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    [Display(Name = "Ngày đăng")]
    [DataType(DataType.Date)]
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; }
    public string Author { get; set; } = string.Empty;
    public int ViewCount { get; set; } = 0;

    // Quan hệ 1-N: Mỗi Post thuộc 1 Category
    public int CategoryId { get; set; }                 // khóa ngoại
    public Category? Category { get; set; }             // navigation

    // Quan hệ N-N: Mỗi Post có nhiều Tag
    public List<Tag> Tags { get; set; } = new();

    public string MoTaNgan() => $"{Title} ({PublishedAt:dd/MM/yyyy})";

    // 👉 THÊM PHẦN MỞ RỘNG CỦA BÀI TẬP VỀ NHÀ:
    public string NhanDanhGia()
    {
        return ViewCount >= 100 ? "Phổ biến" : "Thường";
    }
}