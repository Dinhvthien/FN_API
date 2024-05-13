using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class DangKyHoc
    {
        [Key]
        public int DangKyHocId { get; set; }
        public int? KhoaHocId { get; set; }
        public int? HocVienId { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? TinhTrangHocId { get; set; }
        public int? TaiKhoanId { get; set; }
        public virtual KhoaHoc? KhoaHoc { get; set; }
        public virtual HocVien? HocVien { get; set; }
        public virtual TinhTrangHoc? TinhTrangHoc { get; set; }
        public virtual TaiKhoan? TaiKhoan { get; set; }

    }
}
