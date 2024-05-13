using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class BaiViet
    {
        [Key]
        public int BaiVietId { get; set; }
        [MaxLength(50)]
        public string? TenbaiViet { get; set; }
        [MaxLength(50)]
        public string? TenTacGia { get; set; }
        public string? NoiDung { get; set; }
        [MaxLength(1000)]

        public string? NoiDungNgan { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public string?  HinhAnh { get; set; }
        public int? ChuDeId { get; set; }
        public int? TaiKhoanId { get; set; }
        public virtual ChuDe? ChuDe { get; set; }
        public virtual TaiKhoan? TaiKhoan { get; set; }


    }
}
