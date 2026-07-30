# 🧠 Hệ thống Ngữ cảnh Dự án (Project Context for Claude)
**Dự án:** BlogManager (Môn học: Lập trình ứng dụng Web)
**Namespace chính:** `blogmanager_nguyenngocvi_22t1020794`
**Nền tảng:** ASP.NET Core MVC, .NET SDK (bản LTS)
**Môi trường phát triển:** Google Antigravity IDE (macOS)

---

## 1. Tóm tắt quá trình phát triển (Tiến độ hiện tại)
Dự án đã hoàn thành Buổi 1–3 và đang bước vào **Buổi 4**. Hệ thống hiện có kiến trúc MVC cơ bản với dữ liệu giả (dummy data), LINQ, Bootstrap Grid/Card, và Partial View. Buổi 4 bắt đầu tích hợp cơ sở dữ liệu thật (SQLite + Entity Framework Core). Repository đã được đồng bộ hóa với GitHub.

## 2. Danh sách cấu trúc File (Không viết lại code khi thao tác)
Khi thực hiện các yêu cầu mới, ưu tiên mở rộng và tái sử dụng các file đã có trong danh sách này:
*   `Models/Post.cs` (Model chính: Id, Title, Content, PublishedAt, IsPublished, Author, ViewCount, hàm MoTaNgan, hàm NhanDanhGia)
*   `Models/ErrorViewModel.cs` (Model xử lý lỗi mặc định)
*   `Controllers/HomeController.cs` (Mặc định)
*   `Controllers/LabController.cs` (Thực hành Buổi 2: Xử lý LINQ, ViewBag, ViewData)
*   `Controllers/PostsController.cs` (Thực hành Buổi 3: Truyền Model, hàm GetDummyPosts, action Index, action Details)
*   `Views/Lab/Index.cshtml` (Giao diện hiển thị bảng và thống kê Buổi 2)
*   `Views/Posts/Index.cshtml` (Giao diện danh sách bài viết sử dụng Bootstrap Grid row/col)
*   `Views/Posts/Details.cshtml` (Giao diện xem chi tiết 1 bài viết)
*   `Views/Posts/_PostCard.cshtml` (Partial View: Component thẻ hiển thị bài viết)
*   `Views/Shared/_Layout.cshtml` (Layout chính của toàn bộ ứng dụng)
*   `Views/_ViewImports.cshtml` (Khai báo namespace và Tag Helpers dùng chung)
*   `Views/_ViewStart.cshtml` (Chỉ định Layout mặc định)
*   `Program.cs` (Pipeline cấu hình ứng dụng — **không sửa nếu không phải lỗi pipeline**)
*   `blogmanager_nguyenngocvi_22t1020794.csproj` (File project — target: net10.0)
*   `.gitignore` (Đã cấu hình bỏ qua thư mục `bin/`, `obj/`)

## 3. Nhật ký Lỗi & Cách khắc phục (Troubleshooting Ledger)
*   **Lỗi:** `zsh: command not found: dotnet`
    *   *Nguyên nhân:* Terminal chưa nhận PATH.
    *   *Khắc phục:* Cập nhật export PATH vào `~/.zshrc` hoặc khởi động lại IDE.
*   **Lỗi:** `fatal: not a git repository`
    *   *Nguyên nhân:* Quên chạy lệnh khởi tạo Git cục bộ.
    *   *Khắc phục:* Chạy `git init` -> `git add .` -> `git commit`.
*   **Lỗi:** `Invalid expression term 'in'` trong Razor (`@foreach (var dynamic title in...)`)
    *   *Nguyên nhân:* Khai báo thừa/trùng lặp từ khóa định kiểu.
    *   *Khắc phục:* Chỉ dùng `var` hoặc `string`/`dynamic`, không dùng chung.
*   **Lỗi:** `The view 'Index' was not found.` (Màn hình báo lỗi đường dẫn nền trắng/vàng)
    *   *Phân tích:* Đây là tín hiệu tốt, chứng tỏ Controller và Routing đã hoạt động thành công 100%.
    *   *Khắc phục:* Tạo đúng cấu trúc thư mục `/Views/[Tên_Controller]/[Tên_Action].cshtml`.
*   **Lỗi:** `error CS1061: 'List<Post>' does not contain a definition for 'Any'`
    *   *Nguyên nhân:* Thiếu thư viện LINQ.
    *   *Khắc phục:* Khai báo `@using System.Linq` ở đầu file `.cshtml` hoặc `.cs`.

## 4. Các yêu cầu đã hoàn thành (Chức năng cốt lõi)
*   **Môi trường:** Đẩy code lên GitHub, bỏ qua các thư mục biên dịch cache.
*   **Logic (Model & Controller):** Tạo class `Post` mở rộng (Author, ViewCount). Viết các truy vấn LINQ: Đếm tổng view, tìm bài viết xem nhiều nhất, phân loại nhãn (Phổ biến/Thường). Khởi tạo cơ sở dữ liệu giả (`GetDummyPosts`).
*   **UI/UX (View):** Chuyển đổi dữ liệu sang dạng bảng (Bootstrap Table) -> Tái cấu trúc thành Partial View (`_PostCard`) -> Tổ chức thành lưới (Bootstrap Grid). Chuyển hướng trang bằng Tag Helper (`asp-action`, `asp-route-id`).

## 5. Cẩm nang tiếp cận cho AI Agent (Debugging & Expansion Approach)
Khi AI (Claude/Gemini) đọc file này để tiếp tục phát triển dự án, phải tuân thủ luồng tư duy sau:
*   **Tư duy Debugging:** 
    *   Tuyệt đối không sửa file `Program.cs` nếu không phải là lỗi pipeline.
    *   Tuân thủ nghiêm ngặt quy ước đặt tên (Naming Convention) của ASP.NET Core MVC (Controller phải có hậu tố `Controller`, View phải nằm đúng cấu trúc thư mục).
    *   Sử dụng Tag Helpers (`asp-controller`, `asp-action`) thay vì viết URL tĩnh (`href="/Posts/Details/1"`).
*   **Tư duy Thêm chức năng mới (Feature Expansion):**
    *   *Tách biệt trách nhiệm (Separation of Concerns):* Controller chỉ làm nhiệm vụ lấy dữ liệu và điều hướng. Mọi thao tác tính toán phức tạp phải để ở Model hoặc Service (nếu có sau này).
    *   *Tái sử dụng giao diện:* Tích cực tận dụng Partial View nếu một cụm UI (như form, nút bấm, thẻ) lặp lại nhiều lần.
    *   *Xử lý dữ liệu linh hoạt:* Thay vì dùng `ViewBag` hay `ViewData`, bắt đầu từ Buổi 5 sẽ ưu tiên thiết kế các lớp `ViewModel` chuyên biệt để gom nhóm dữ liệu truyền sang View.

## 6. Lộ trình phát triển (Roadmap)
Dựa theo giáo trình 11 buổi, dự án cần chuẩn bị tích hợp các giai đoạn sau:

### 🔧 Buổi 4 — Tích hợp Database (TODO — Đang thực hiện)
*   [x] Cài package `Microsoft.EntityFrameworkCore`, `.Design`, `.Sqlite` (v10.0.10) + tool `dotnet-ef` (v10.0.10)
*   [x] Tạo file `Data/ApplicationDbContext.cs` — kế thừa `DbContext`, khai báo `DbSet<Post>`
*   [x] Cập nhật `Program.cs` — đăng ký `ApplicationDbContext` vào DI container với connection string SQLite
*   [x] Chạy migration đầu tiên: `dotnet ef migrations add KhoiTao` → `dotnet ef database update` → tạo `blogmanager.db`
*   [ ] Tạo file `Data/DbInitializer.cs` — seed dữ liệu mẫu từ `GetDummyPosts()` vào database khi DB trống
*   [ ] Refactor `PostsController.cs` — thay `GetDummyPosts()` bằng truy vấn từ `ApplicationDbContext` (Dependency Injection)
*   [ ] Kiểm tra action `Index` và `Details` hoạt động với dữ liệu từ SQLite

### 📝 Buổi 5–6 — CRUD + Tìm kiếm + Phân trang (TODO)
*   [ ] Thêm action `Create` (GET + POST) vào `PostsController` — form tạo bài viết mới
*   [ ] Thêm action `Edit` (GET + POST) — form chỉnh sửa bài viết
*   [ ] Thêm action `Delete` (GET + POST) — xác nhận và xóa bài viết
*   [ ] Tạo các View tương ứng: `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`
*   [ ] Thêm Data Annotations vào `Post.cs` (`[Required]`, `[StringLength]`, `[Display]`) để validate form
*   [ ] Implement tìm kiếm (Search) theo Title/Author
*   [ ] Implement phân trang (Pagination) cho danh sách bài viết
*   [ ] Thiết kế `ViewModel` chuyên biệt nếu cần gom dữ liệu phức tạp

### 🔐 Buổi 7 — Identity & Phân quyền (TODO)
*   [ ] Tích hợp ASP.NET Core Identity (Đăng nhập, đăng ký)
*   [ ] Phân quyền Admin/User
*   [ ] Bảo vệ các action CRUD (chỉ Admin hoặc chủ bài viết mới được sửa/xóa)

### 🚀 Buổi 10–11 — Deploy & API (TODO)
*   [ ] Đóng gói, triển khai (Deploy) website lên môi trường thực tế (Cloud)
*   [ ] Xây dựng RESTful API để cấp dữ liệu cho thiết bị ngoại vi (Mobile/Client khác)
