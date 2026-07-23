namespace blogmanager_nguyenngocvi_22t1020794.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; }
    
    // Thêm 2 thuộc tính mới theo yêu cầu đề bài:
    public string Author { get; set; } = string.Empty;
    public int ViewCount { get; set; } = 0;

    public string MoTaNgan() => $"{Title} ({PublishedAt:dd/MM/yyyy})";
}