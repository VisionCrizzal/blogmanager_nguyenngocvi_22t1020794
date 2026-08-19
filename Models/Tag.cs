using System.ComponentModel.DataAnnotations;

namespace blogmanager_nguyenngocvi_22t1020794.Models;

public class Tag
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên thẻ là bắt buộc")]
    [StringLength(50, MinimumLength = 1)]
    [Display(Name = "Thẻ")]
    public string Name { get; set; } = string.Empty;

    // Navigation N-N: Một Tag gắn nhiều Post
    public List<Post> Posts { get; set; } = new();
}
