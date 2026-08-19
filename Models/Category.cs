using System.ComponentModel.DataAnnotations;

namespace blogmanager_nguyenngocvi_22t1020794.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên chuyên mục là bắt buộc")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên chuyên mục từ 2 đến 100 ký tự")]
    [Display(Name = "Tên chuyên mục")]
    public string Name { get; set; } = string.Empty;

    // Navigation 1-N: Một Category có nhiều Post
    public List<Post> Posts { get; set; } = new();
}
