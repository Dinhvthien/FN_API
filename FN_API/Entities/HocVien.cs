using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class HocVien
    {
        [Key]
        public int HocVienId { get; set; }
        public string? HinhAnh { get; set; }
        [MaxLength(50)]
        public string? Hoten { get; set; }
        public DateTime? NgaySinh { get; set; }
        [MaxLength(11)]
        public string? SoDienThoai { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        [MaxLength(50)]
        public string? TinhThanh { get; set; }
        [MaxLength(50)]
        public string? QuanHuyen { get; set; }
        [MaxLength(50)]
        public string? PhuongXa { get; set; }
        [MaxLength(50)]
        public string? SoNha { get; set; }
        public virtual List<DangKyHoc>? DangKyHocs { get; set; }
    }
}
