# 🚀 Hệ Thống Quản Lý Nhân Sự (Employee Management System)

## 📌 Giới thiệu
Employee Management System là hệ thống quản lý nhân sự hiện đại, hỗ trợ doanh nghiệp quản lý nhân viên, chấm công, tính lương, thưởng phạt, nghỉ phép, tăng ca và theo dõi sức khỏe nhân viên. Hệ thống được phát triển với **.NET Core 8 API** và **Blazor WebAssembly**, triển khai trên **Azure Cloud**.

## 🔥 Tính năng chính
- 📋 **Quản lý nhân viên**: Thêm, sửa, xóa và tìm kiếm nhân viên.
- 🎁 **Thưởng & phạt**: Quản lý các khoản thưởng, kỷ luật nhân viên.
- 🏖 **Nghỉ phép**: Theo dõi và duyệt đơn nghỉ phép.
- ⏳ **Tăng ca**: Ghi nhận giờ làm thêm 
- ❤️ **Sức khỏe nhân viên**: Quản lý thông tin sức khỏe và lịch sử khám bệnh.

## 🛠 Công nghệ sử dụng
- **Backend**: ASP.NET Core 8 Web API
- **Frontend**: Blazor WebAssembly
- **Cơ sở dữ liệu**: SQL Server
- **Triển khai**: Azure App Service, Azure Static Web Apps

## 🚀 Cài đặt và chạy dự án
### Yêu cầu hệ thống:
- .NET SDK 8.0
- SQL Server
- Visual Studio 2022 / VS Code
- Azure Account (nếu triển khai lên cloud)

### Cách chạy dự án cục bộ:
1. **Clone repository**:
   ```sh
   git clone https://github.com/yourusername/employee-management-system.git
   cd employee-management-system
   ```
2. **Cấu hình kết nối CSDL** (trong `appsettings.json` của backend).
3. **Chạy backend**:
   ```sh
   cd Backend
   dotnet run
   ```
4. **Chạy frontend**:
   ```sh
   cd Frontend
   dotnet run
   ```
5. Mở trình duyệt và truy cập `https://localhost:5001`

## 📢 Triển khai lên Azure
1. **Backend**: Deploy lên **Azure App Service**.
2. **Frontend**: Deploy lên **Azure Static Web Apps**.
3. **Database**: Sử dụng **Azure SQL Database**.

## 📜 Giấy phép
Dự án này được phát hành theo [MIT License](LICENSE).

---

💡 **Liên hệ & Đóng góp**
Nếu bạn có bất kỳ ý tưởng hoặc gặp vấn đề, vui lòng tạo **Issue** hoặc gửi **Pull Request**! 🙌

