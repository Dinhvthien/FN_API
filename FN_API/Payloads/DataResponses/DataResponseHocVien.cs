using System.ComponentModel.DataAnnotations;

namespace FN_API.Payloads.DataResponses
{
    public class DataResponseHocVien
    {
        public int? HocVienId { get; set; }
        public string? HinhAnh { get; set; }
        public string? Hoten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? TinhThanh { get; set; }
        public string? QuanHuyen { get; set; }
        public string? PhuongXa { get; set; }
        public string? SoNha { get; set; }
    }
}
