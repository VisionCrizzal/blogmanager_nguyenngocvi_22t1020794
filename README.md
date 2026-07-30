# BlogManager - Dự án môn học Lập trình ứng dụng Web

Dự án này được xây dựng xuyên suốt môn học **Lập trình ứng dụng Web**. Đây là hệ thống quản lý blog cá nhân được phát triển bằng nền tảng ASP.NET Core MVC.

## 🎓 Thông tin sinh viên
- **Họ và tên:** Nguyễn Ngọc Vĩ
- **Mã sinh viên:** 22T1020794

## 🛠 Công nghệ sử dụng
- **Framework:** ASP.NET Core MVC (.NET 10)
- **Cơ sở dữ liệu:** SQLite thông qua Entity Framework Core
- **Giao diện:** HTML/CSS, Bootstrap, Razor View Engine
- **Môi trường phát triển:** Antigravity IDE (macOS)

## 🚀 Tiến độ học tập & Phát triển
- [x] **Buổi 1-3:** Khởi tạo kiến trúc MVC, tạo Model (Post), xây dựng giao diện hiển thị danh sách bài viết với Bootstrap Grid, làm quen với LINQ và Partial View.
- [x] **Buổi 4:** Tích hợp cơ sở dữ liệu thật (SQLite + EF Core), thực hiện Migration (tạo bảng Posts và Categories), và cấu hình seed dữ liệu mẫu (`HasData`).
- [ ] **Buổi 5-6:** Triển khai các tính năng CRUD đầy đủ (Thêm, sửa, xóa), xử lý form validation, tìm kiếm và phân trang.
- [ ] **Buổi 7:** Tích hợp Identity để quản lý Đăng nhập, Đăng ký và phân quyền người dùng (Admin/User).
- [ ] **Buổi 10-11:** Triển khai (Deploy) dự án lên Cloud và phát triển RESTful API để cung cấp dữ liệu cho thiết bị khác.

## ⚙️ Hướng dẫn cài đặt và chạy thử nghiệm
1. Clone project từ GitHub về máy cục bộ.
2. Mở Terminal tại thư mục chứa file `.csproj`.
3. Cập nhật cơ sở dữ liệu SQLite cục bộ bằng công cụ `dotnet-ef`:
   ```bash
   dotnet ef database update
   ```
4. Khởi chạy ứng dụng:
   ```bash
   dotnet run
   ```
5. Mở trình duyệt và truy cập vào đường dẫn `http://localhost:xxxx` (port hiển thị trên Terminal).
