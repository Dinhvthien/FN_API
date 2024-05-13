using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FN_API.Entities
{
    public class TaiKhoan
    {
        [Key]
        public int TaiKhoanId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? TenNguoiDung { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? TenDangNhap { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? MatKhau { get; set; }
        public int? QuyenHanId { get; set; }
        public virtual QuyenHan? QuyenHan { get; set; }
    }
}
