using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FN_API.Payloads.DataRequests
{
    public class Data_RequestTaiKhoan
    {
        public int TaiKhoanId { get; set; }
        public string? TenNguoiDung { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public int? QuyenHanId { get; set; }
    }
}
