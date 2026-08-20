# 🧠 Hệ thống Ngữ cảnh Dự án (Project Context for Claude)
**Dự án:** BlogManager (Môn học: Lập trình ứng dụng Web)
**Namespace chính:** `blogmanager_nguyenngocvi_22t1020794`
**Nền tảng:** ASP.NET Core MVC, .NET SDK (bản LTS)
**Môi trường phát triển:** Google Antigravity IDE (macOS)

---

## 1. Tóm tắt quá trình phát triển (Tiến độ hiện tại)
Dự án đã hoàn thành **Buổi 1–5**. Hệ thống hiện có kiến trúc MVC hoàn chỉnh với cơ sở dữ liệu SQLite (Entity Framework Core), CRUD đầy đủ (Create/Read/Update/Delete), Data Annotations validation, Dark Mode (Catppuccin Frappé), và giao diện responsive hỗ trợ điện thoại. Repository được đồng bộ hóa với GitHub.

## 2. Danh sách cấu trúc File (Không viết lại code khi thao tác)
Khi thực hiện các yêu cầu mới, ưu tiên mở rộng và tái sử dụng các file đã có trong danh sách này:
*   `Models/Post.cs` (Model chính: Id, Title, Content, PublishedAt, IsPublished, Author, ViewCount, hàm MoTaNgan, hàm NhanDanhGia + Data Annotations: `[Required]`, `[StringLength]`, `[Display]`, `[DataType]`)
*   `Models/ErrorViewModel.cs` (Model xử lý lỗi mặc định)
*   `Controllers/HomeController.cs` (Mặc định)
*   `Controllers/LabController.cs` (Thực hành Buổi 2: Xử lý LINQ, ViewBag, ViewData)
*   `Controllers/PostsController.cs` (CRUD hoàn chỉnh: Index, Details, Create, Edit, Delete — tất cả async/await)
*   `Data/ApplicationDbContext.cs` (DbContext kết nối SQLite, khai báo DbSet<Post>)
*   `Views/Lab/Index.cshtml` (Giao diện hiển thị bảng và thống kê Buổi 2)
*   `Views/Posts/Index.cshtml` (Giao diện danh sách bài viết — Bootstrap Grid, nút Tạo mới)
*   `Views/Posts/Details.cshtml` (Giao diện xem chi tiết 1 bài viết — metadata tách dòng cho mobile)
*   `Views/Posts/Create.cshtml` (Form tạo bài viết mới — Tag Helpers + validation)
*   `Views/Posts/Edit.cshtml` (Form chỉnh sửa bài viết — Tag Helpers + validation)
*   `Views/Posts/Delete.cshtml` (Trang xác nhận xoá bài viết)
*   `Views/Posts/_PostCard.cshtml` (Partial View: Component thẻ hiển thị bài viết + nút Xem/Sửa/Xoá)
*   `Views/Shared/_Layout.cshtml` (Layout chính: navbar, hamburger menu, dark mode toggle)
*   `Views/_ViewImports.cshtml` (Khai báo namespace và Tag Helpers dùng chung)
*   `Views/_ViewStart.cshtml` (Chỉ định Layout mặc định)
*   `wwwroot/css/site.css` (CSS tuỳ chỉnh: Catppuccin Frappé dark mode + responsive mobile)
*   `wwwroot/js/site.js` (JavaScript: xử lý toggle dark/light mode)
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
*   **Lỗi:** Form Edit bấm "Lưu thay đổi" nhưng không xảy ra gì (trang đứng yên, không redirect)
    *   *Nguyên nhân:* ASP.NET Core tự coi các thuộc tính `string` non-nullable là `[Required]`. Trường `Author` bị giấu bằng `<input type="hidden">` với giá trị rỗng → `ModelState.IsValid = false`. Đồng thời `asp-validation-summary="ModelOnly"` ẩn luôn thông báo lỗi → người dùng không thấy gì.
    *   *Khắc phục:* (1) Đổi `asp-validation-summary` thành `"All"` để hiện đầy đủ lỗi. (2) Hiển thị ô nhập cho Author thay vì dùng hidden field rỗng.

## 4. Các yêu cầu đã hoàn thành (Chức năng cốt lõi)
*   **Môi trường:** Đẩy code lên GitHub, bỏ qua các thư mục biên dịch cache.
*   **Logic (Model & Controller):** Tạo class `Post` mở rộng (Author, ViewCount). Viết các truy vấn LINQ. CRUD đầy đủ với async/await. Data Annotations validation (`[Required]`, `[StringLength]`, `[Display]`).
*   **Database:** SQLite + Entity Framework Core. DI tiêm `ApplicationDbContext` vào Controller. Migration: `KhoiTao`, `SeedPosts`.
*   **UI/UX (View):** Bootstrap Grid/Card, Partial View (`_PostCard`), Tag Helpers. Form Create/Edit/Delete với validation client-side (`_ValidationScriptsPartial`). Dark Mode Catppuccin Frappé. Responsive mobile (breakpoint 576px, 768px, 992px).

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

### ✅ Buổi 4 — Tích hợp Database (DONE)
*   [x] Cài package `Microsoft.EntityFrameworkCore`, `.Design`, `.Sqlite` (v10.0.10) + tool `dotnet-ef` (v10.0.10)
*   [x] Tạo file `Data/ApplicationDbContext.cs` — kế thừa `DbContext`, khai báo `DbSet<Post>`
*   [x] Cập nhật `Program.cs` — đăng ký `ApplicationDbContext` vào DI container với connection string SQLite
*   [x] Chạy migration đầu tiên: `dotnet ef migrations add KhoiTao` → `dotnet ef database update` → tạo `blogmanager.db`
*   [x] Thêm dữ liệu mẫu (seed) cho Posts bằng `HasData()` trong `OnModelCreating`, migration `SeedPosts` và update
*   [x] Refactor `PostsController.cs` — thay `GetDummyPosts()` bằng truy vấn từ `ApplicationDbContext` (Dependency Injection)
*   [x] Kiểm tra action `Index` và `Details` hoạt động với dữ liệu từ SQLite

### ✅ Buổi 5–6 — CRUD, Quan hệ, Tìm kiếm, Sắp xếp, Phân trang (DONE)
*   [x] Thêm action `Create` (GET + POST) vào `PostsController` — form tạo bài viết mới
*   [x] Thêm action `Edit` (GET + POST) — form chỉnh sửa bài viết
*   [x] Thêm action `Delete` (GET + POST) — xác nhận và xóa bài viết
*   [x] Tạo các View tương ứng: `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`
*   [x] Thêm Data Annotations vào `Post.cs` (`[Required]`, `[StringLength]`, `[Display]`) để validate form
*   [x] Sửa lỗi validation form Edit (Author bị null → ModelState invalid)
*   [x] Tối ưu responsive mobile toàn bộ web (CSS media queries)
*   [x] Thiết lập quan hệ 1-N (Post-Category) và N-N (Post-Tag), tạo bảng nối `PostTag`
*   [x] Cập nhật DbContext, chạy migration và update database
*   [x] Sử dụng `Include` (Eager Loading) để nạp dữ liệu liên kết Category và Tags
*   [x] Nâng cấp tìm kiếm: Lọc theo Category và Từ khóa
*   [x] Thêm Sắp xếp (Sort) bằng LINQ `OrderBy` / `OrderByDescending` (theo Mới nhất, Cũ nhất, Tiêu đề)
*   [x] Cập nhật Form Create/Edit: Thêm Dropdown chọn Category và Checkbox tạo/chọn Tags
*   [x] Implement phân trang (Pagination) kết hợp giữ nguyên bộ lọc và sắp xếp
*   [x] Thiết kế `ViewModel` chuyên biệt (`PostListViewModel`) thay thế ViewBag để gom nhóm dữ liệu phân trang, lọc, danh sách

### 🔐 Buổi 7 — Identity & Phân quyền (TODO)
> ⚠️ **Lưu ý:** Buổi này mình bị hỏng màn Mac nên không thực hành được trên lớp, cần làm bù kỹ hơn.

**Phần 1 — Xác thực vs Phân quyền; Identity (25 phút lý thuyết)**
*   [ ] Hiểu khái niệm Authentication (xác thực — "bạn là ai?") vs Authorization (phân quyền — "bạn được làm gì?")
*   [ ] Tổng quan ASP.NET Core Identity: hệ thống quản lý User, Role, Claims tích hợp sẵn

**Phần 2 — Cấu hình Identity + hash mật khẩu (30 phút)**
*   [x] Cài package `Microsoft.AspNetCore.Identity.EntityFrameworkCore` + `Microsoft.AspNetCore.Identity.UI`
*   [x] Đổi `ApplicationDbContext` kế thừa `IdentityDbContext` thay vì `DbContext`
*   [x] Đăng ký Identity vào `Program.cs` (`AddDefaultIdentity<IdentityUser>()` + `AddRoles` + middleware)
*   [x] Chạy migration `AddIdentity` → tạo bảng AspNetUsers, AspNetRoles, ...
*   [x] Hiểu cơ chế hash mật khẩu (không lưu plaintext — cột PasswordHash trong AspNetUsers)

**Phần 3 — `[Authorize]` + vai trò (25 phút)**
*   [x] Gắn `[Authorize]` lên Controller PostsController để yêu cầu đăng nhập
*   [x] Dùng `[AllowAnonymous]` cho các trang công khai (Index, Details)
*   [x] Dùng `[Authorize(Roles = "Admin")]` để giới hạn Delete chỉ cho Admin
*   [x] Ẩn/hiện nút trong View theo vai trò (Thêm: đăng nhập, Sửa: đăng nhập, Xóa: Admin)

**Phần 4 — Seed Role & Admin (20 phút)**
*   [x] Tạo sẵn các Role (Admin, User) khi khởi động ứng dụng
*   [x] Seed tài khoản Admin mặc định (admin@blogmanager.local / Admin@123)
*   [x] Gán Role "Admin" cho tài khoản seed

**Phần 5 — Live coding: Tích hợp + phân quyền 3 mức (40 phút)**
*   [x] Tích hợp toàn bộ Identity vào project BlogManager
*   [x] Phân quyền 3 mức: Anonymous (xem), User (tạo bài), Admin (sửa/xóa tất cả)
*   [x] Tạo giao diện Đăng nhập / Đăng ký / Đăng xuất (Identity.UI Razor Pages)
*   [x] Hiển thị nút Login/Logout/Register trên thanh điều hướng (navbar — `_LoginPartial`)

**Phần 6 — Thực hành trên lớp + Git + tổng kết (30 phút)**
> 📝 **Đề bài:** Tích hợp Identity với 3 mức quyền. Đầu ra: ảnh chụp ở cả 3 trạng thái + commit.

*   [ ] Kiểm tra: Chưa đăng nhập → xem được /Posts, Details; bấm Thêm/Sửa → chuyển trang đăng nhập
*   [ ] Kiểm tra: User tự đăng ký → đăng nhập → Thêm/Sửa được; **không** thấy nút Xóa
*   [ ] Kiểm tra: Admin (admin@blogmanager.local / Admin@123) → thấy và dùng được nút Xóa
*   [x] `git add .` → `git commit -m "Add Identity authentication and role-based authorization"`
> ✅ Code đã hoàn thành 100%. Chỉ còn test thủ công trên trình duyệt và chụp ảnh nộp bài.

**Mở rộng Buổi 7 — OwnerId (chủ sở hữu bài viết)**
*   [x] Thêm `OwnerId` (string? FK đến IdentityUser) vào model `Post`
*   [x] Migration `AddOwnerIdToPost` → cột OwnerId trong bảng Posts
*   [x] Gán `OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier)` khi tạo bài mới
*   [x] Kiểm tra phía server: chỉ **chủ sở hữu** hoặc **Admin** mới được sửa/xóa bài cụ thể
*   [x] `git commit -m "Add OwnerId ownership check - only owner or Admin can edit/delete post"`

### 🚀 Buổi 11 — RESTful API với ASP.NET Core, EF Core và Swagger (TODO)
> 📚 Buổi này thầy cho tự học ở nhà. Thời lượng: 3 giờ.

**Phần 1 — REST, HTTP method, mã trạng thái (25 phút lý thuyết)**
*   [x] Hiểu REST là gì: kiến trúc giao tiếp Client-Server qua HTTP
*   [x] Nắm các HTTP method: GET (lấy), POST (tạo), PUT (cập nhật), DELETE (xóa)
*   [x] Các mã trạng thái thường dùng: 200 OK, 201 Created, 204 NoContent, 400 BadRequest, 404 NotFound, 401/403

**Phần 2 — API Controller + DTO (30 phút)**
*   [x] Tạo API Controller riêng (dùng `[ApiController]` + `[Route("api/[controller]")]`)
*   [x] Đặt cùng ứng dụng MVC (chung project, khác folder Controllers/Api)
*   [x] Tạo DTO (Data Transfer Object) để tách biệt dữ liệu API trả về với Model gốc

**Phần 3 — CRUD API với EF Core (30 phút)**
*   [x] GET `/api/posts` — lấy danh sách bài viết
*   [x] GET `/api/posts/{id}` — lấy chi tiết 1 bài viết
*   [x] POST `/api/posts` — tạo bài viết mới (nhận DTO, trả 201 Created)
*   [x] PUT `/api/posts/{id}` — cập nhật bài viết (trả 204 NoContent)
*   [x] DELETE `/api/posts/{id}` — xóa bài viết (trả 204 NoContent)
*   [x] Thêm validation cho DTO

**Phần 4 — Swagger + bảo mật API (20 phút)**
*   [x] Cài và cấu hình Swagger UI (`Swashbuckle.AspNetCore`)
*   [x] Truy cập `/swagger` để xem tài liệu API tự động
*   [x] Bảo mật API: gắn `[Authorize]` cho các endpoint cần xác thực

**Phần 5 — Live coding: API posts + kiểm thử Swagger (40 phút)**
*   [x] Tạo `PostsApiController` hoàn chỉnh với CRUD
*   [x] Tạo `PostDto` cho request/response
*   [x] Kiểm thử trên Swagger UI: thử GET, POST, PUT, DELETE

**Phần 6 — Thực hành + tổng kết dự án (30 phút)**
*   [x] Test API qua Swagger UI
*   [x] `git add .` → `git commit -m "Add RESTful API for posts with DTO, validation and Swagger"`

## 7. Ý tưởng Mở rộng & Cảm hứng (Creative Playground)
Nếu có thời gian rảnh rỗi và muốn làm gì đó "vượt ra ngoài bài giảng" để thoả mãn đam mê code, đây là một số ý tưởng cực hay để nâng cấp giao diện và tính năng cho dự án:
*   ✨ **Giao diện Modern & Glassmorphism:** Áp dụng hiệu ứng kính mờ (`backdrop-filter`) cho các Card bài viết, làm hiệu ứng hover nổi bật (scale nhẹ 1.02x, đổ bóng `box-shadow` mượt mà).
*   🌓 **Chế độ Tối/Sáng (Dark/Light Mode):** Tích hợp nút toggle chuyển đổi giao diện và tự động bắt theo prefer-color-scheme của hệ điều hành.
*   🏷 **Hệ thống Badge Động:** Hiển thị màu sắc gradient khác nhau cho từng Category (ví dụ: "C#" màu tím, "MVC" màu lục lam).
*   📝 **Trình soạn thảo Markdown/Rich Text:** Thay vì dùng form nhập liệu thô sơ, tích hợp ToastUI Editor, TinyMCE, hoặc EasyMDE để soạn thảo bài viết xịn xò.
*   💬 **Hệ thống Bình luận (Comments):** Tạo tính năng bình luận dưới mỗi bài viết, tích hợp avatar ngẫu nhiên (như DiceBear API).
*   🔔 **Toast Notifications cực mượt:** Dùng thư viện như SweetAlert2 hoặc Toastr thay thế cho thông báo `alert()` nhàm chán của trình duyệt.
*   🎨 **Font chữ ấn tượng:** Sử dụng Google Fonts (như `Inter`, `Outfit`, hoặc `Plus Jakarta Sans`) kết hợp với typography hiện đại.
*   🚀 **Hiệu ứng Scroll (Scroll Reveal):** Các thẻ bài viết tự động trượt lên, hiện ra từ từ khi người dùng cuộn trang (sử dụng thư viện AOS hoặc Intersection Observer API).

## 8. Skill UI/UX Pro Max (Đã cài đặt)
Dự án đã tích hợp skill **UI/UX Pro Max** tại `.agent/skills/ui-ux-pro-max/`. Đây là bộ trí tuệ thiết kế chứa 67 phong cách UI, 96 bảng màu, 57 cặp font, 99 guideline UX, và 25 loại biểu đồ.

### Khi nào sử dụng?
Khi nhận yêu cầu liên quan đến giao diện — thiết kế mới, nâng cấp UI, chọn màu sắc/font, hoặc triển khai trang Landing Page — **bắt buộc** chạy skill này trước khi code.

### Cách sử dụng (Quy trình 4 bước)
1.  **Tạo Design System (Bắt buộc):**
    ```bash
    python3 .agent/skills/ui-ux-pro-max/scripts/search.py "<từ khóa mô tả>" --design-system -p "BlogManager"
    ```
2.  **Tra cứu chi tiết theo domain** (nếu cần thêm):
    ```bash
    python3 .agent/skills/ui-ux-pro-max/scripts/search.py "<từ khóa>" --domain <style|typography|color|ux|chart|landing>
    ```
3.  **Lưu Design System** (để dùng xuyên phiên làm việc):
    ```bash
    python3 .agent/skills/ui-ux-pro-max/scripts/search.py "<từ khóa>" --design-system --persist -p "BlogManager"
    ```
4.  **Checklist trước khi giao UI:**
    *   Không dùng emoji làm icon UI (dùng SVG: Heroicons, Lucide)
    *   Tất cả phần tử bấm được phải có `cursor: pointer`
    *   Kiểm tra Dark Mode Catppuccin Frappé — chữ phải đủ tương phản (4.5:1)
    *   Responsive ở 375px, 768px, 1024px, 1440px
    *   Không bị cuộn ngang trên mobile

### Lưu ý cho dự án này
*   Stack mặc định: **ASP.NET Core MVC + Bootstrap** (không dùng Tailwind trừ khi có yêu cầu)
*   Dark Mode hiện tại: **Catppuccin Frappé** — mọi thay đổi giao diện phải tương thích cả 2 chế độ sáng/tối
*   Tránh fix cứng màu chữ bằng inline style (đã gặp lỗi ở Views/Posts trước đó)
*   **Responsive Mobile:** Đã tối ưu toàn bộ web cho điện thoại (≤576px) và tablet (577–991px). Khi thêm trang mới phải kiểm tra ở cả 3 breakpoint: 375px (mobile), 768px (tablet), 1440px (desktop). Chi tiết CSS nằm trong `wwwroot/css/site.css` phần `/* ===== Mobile Responsive ===== */`

### Sự phối hợp hoàn hảo: UI UX Pro Max & Magic MCP
Cùng với UI/UX Pro Max, hệ thống đã được cài đặt **Magic MCP (@21st-dev/magic)** để tạo thành combo thiết kế - lập trình tự động:

*   **UI UX Pro Max (Kiến trúc sư thiết kế):** Là Skill (file-based). Đảm nhận việc cung cấp Style, Color, Font, và các UX rules. Hoạt động offline không cần internet.
*   **Magic MCP (Kho vật liệu xây dựng):** Là MCP Server (API-based). Đảm nhận việc cung cấp kho Code components React + Tailwind CSS sẵn dùng từ 21st.dev. Cần kết nối internet.

**Quy trình phối hợp (Tự động hóa hoàn toàn bởi AI):**
1.  **Bạn yêu cầu:** Ví dụ "Tạo landing page cho startup fintech".
2.  **UI UX Pro Max phân tích:** AI sử dụng Skill để tạo Design System hoàn chỉnh (chọn Glassmorphism, Dark Mode, bảng màu, font chữ...).
3.  **Magic MCP tìm Components:** AI gọi API của 21st.dev để tìm và lấy code sẵn của Hero section, Feature grid, Testimonial slider, v.v.
4.  **AI kết hợp & Build:** AI lấy components từ Magic MCP, sau đó tùy chỉnh màu sắc, khoảng cách, font chữ theo đúng Design System của UI UX Pro Max, và tự động tích hợp code vào dự án.

## 9. Các Skill Bổ trợ Chất lượng Code & Hiệu năng (Đã cài đặt)
Ngoài skill UI/UX, dự án còn được trang bị 5 skill hỗ trợ kiểm soát chất lượng code và hiệu năng tại `.agent/skills/`:

### 9.1. Code Review AI (`code-review-ai-ai-review`)
**Mục đích:** Review code tự động kết hợp phân tích tĩnh (SonarQube, CodeQL, Semgrep) và AI (Claude/GPT) để phát hiện lỗi bảo mật, hiệu năng, kiến trúc.
*   **Khi nào dùng:** Khi cần review Pull Request, kiểm tra bảo mật (OWASP Top 10), phát hiện N+1 query, hoặc đánh giá chất lượng code trước khi merge.
*   **Phân loại mức độ:** CRITICAL → HIGH → MEDIUM → LOW → INFO
*   **Lưu ý cho dự án:** Đặc biệt hữu ích khi tích hợp Entity Framework Core — giúp phát hiện vấn đề N+1 query, thiếu index, và SQL injection tiềm ẩn.

### 9.2. Code Refactoring (`code-refactoring-refactor-clean`)
**Mục đích:** Phân tích và tái cấu trúc code theo nguyên tắc Clean Code và SOLID, giảm code smell, tăng khả năng bảo trì.
*   **Khi nào dùng:** Khi code bị rối, trùng lặp nhiều, hoặc cần chuẩn bị module cho tính năng mới.
*   **Không dùng khi:** Chỉ cần sửa 1 dòng nhỏ, hoặc đang trong giai đoạn "change freeze".
*   **Quy trình:** Đánh giá code smell → Lập kế hoạch refactor từng bước → Áp dụng thay đổi nhỏ → Chạy test kiểm tra regression.

### 9.3. Codebase Cleanup & Tech Debt (`codebase-cleanup-tech-debt`)
**Mục đích:** Nhận diện, đo lường và lập kế hoạch xử lý Technical Debt (nợ kỹ thuật) trong dự án.
*   **Khi nào dùng:** Khi cần đánh giá tổng thể sức khoẻ codebase, lập roadmap dọn dẹp code, hoặc báo cáo nợ kỹ thuật.
*   **Phân loại nợ:** Code Debt (trùng lặp, phức tạp) → Architecture Debt (thiết kế sai) → Testing Debt (thiếu test) → Documentation Debt → Infrastructure Debt.
*   **Đầu ra:** Bảng kiểm kê nợ kỹ thuật, phân tích tác động (ROI), roadmap ưu tiên theo quý, Quick Wins cho sprint hiện tại.

### 9.4. Web Performance Optimization (`web_performance_optimization`)
**Mục đích:** Tối ưu hiệu năng website toàn diện — tốc độ tải trang, Core Web Vitals (LCP, FID, CLS), kích thước bundle, caching, và runtime performance. Phiên bản mới (647 dòng) cực kỳ chi tiết với ví dụ code thực tế, checklist, và danh sách công cụ đo lường.
*   **Khi nào dùng:** Khi trang web tải chậm, điểm Lighthouse thấp, hoặc cần tối ưu trước khi deploy lên production. Đặc biệt hữu ích khi chuẩn bị cho Buổi 10–11 (Deploy).
*   **Không dùng khi:** Đang trong giai đoạn prototype/MVP, chưa có người dùng thực.
*   **Quy trình 5 bước:** Đo hiệu năng (Lighthouse) → Xác định vấn đề → Ưu tiên tối ưu → Implement (hình ảnh WebP/AVIF, lazy load, code splitting, critical CSS, caching) → Verify bằng số liệu trước/sau.
*   **Bao gồm:**
    *   Ví dụ chi tiết: Tối ưu Core Web Vitals, giảm bundle JS, tối ưu hình ảnh
    *   Best Practices (Do / Don't) và Common Pitfalls
    *   Performance Checklist (Images, JS, CSS, Caching, Core Web Vitals)
    *   Danh sách công cụ: Lighthouse, WebPageTest, webpack-bundle-analyzer, PageSpeed Insights, v.v.
*   **Lưu ý cho dự án:** Hỗ trợ tối ưu cả Frontend (hình ảnh, JS, CSS) và Backend (EF Core query, caching, response compression). Kết hợp được với Chrome DevTools MCP để đo LCP, CLS trực tiếp.
*   **⚠️ Lưu ý tương thích:** Skill này được thiết kế gốc cho **Claude Code** (terminal-based). Trên **Antigravity IDE**, hầu hết nội dung vẫn hoạt động tốt (hướng dẫn tối ưu, checklist, ví dụ code). Tuy nhiên, một số lệnh CLI cụ thể (ví dụ: `lighthouse`, `webpack-bundle-analyzer`) có thể cần điều chỉnh cách chạy tùy vào môi trường. Các phần liên quan đến React/Next.js chỉ mang tính tham khảo vì dự án này dùng ASP.NET Core MVC.

### 9.5. Error Handling Patterns (`error-handling-patterns`)
**Mục đích:** Hướng dẫn xây dựng ứng dụng bền vững với các chiến lược xử lý lỗi chuyên nghiệp — Exceptions, Result Types, Error Propagation, Circuit Breaker, và Graceful Degradation. Bao gồm SKILL.md (122 dòng) và `references/details.md` (517 dòng) với ví dụ code đa ngôn ngữ.
*   **Khi nào dùng:** Khi thiết kế xử lý lỗi cho feature mới, xây dựng API, debug lỗi production, hoặc cải thiện độ ổn định ứng dụng.
*   **Không dùng khi:** Chỉ cần sửa 1 lỗi nhỏ đơn lẻ, hoặc đang làm prototype nhanh không cần error handling tỉ mỉ.
*   **Nội dung chính:**
    *   Triết lý: Exceptions vs Result Types vs Error Codes vs Option/Maybe
    *   Phân loại lỗi: Recoverable (network timeout, invalid input) vs Unrecoverable (OOM, null pointer)
    *   Ví dụ code: Python, TypeScript/JavaScript, Rust, Go
    *   3 Universal Patterns: Circuit Breaker, Error Aggregation, Graceful Degradation
    *   8 Best Practices: Fail Fast, Preserve Context, Meaningful Messages, Clean Up Resources...
    *   7 Common Pitfalls: Catching Too Broadly, Empty Catch Blocks, Logging and Re-throwing...
*   **Lưu ý cho dự án:** Dùng C# nên áp dụng tư duy Exception Hierarchy (giống mẫu Python/TypeScript trong skill). Đặc biệt hữu ích khi xây dựng API (Buổi 10–11) và xử lý lỗi trong Controller/Service layer.
*   **⚠️ Lưu ý tương thích:** Skill gốc từ **Claude Code**. Ví dụ code là Python/TS/Rust/Go, không có C# trực tiếp — nhưng các pattern (Circuit Breaker, Retry, Graceful Degradation) áp dụng được cho mọi ngôn ngữ. AI Agent cần tự chuyển đổi sang C# khi implement.
