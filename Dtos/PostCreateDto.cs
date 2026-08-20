using System.ComponentModel.DataAnnotations;

namespace blogmanager_nguyenngocvi_22t1020794.Dtos
{
    public class PostCreateDto
    {
        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        public string Content { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;
    }
}
