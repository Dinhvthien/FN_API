using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class QuyenHan
    {
        [Key]
        public int QuyenHanId { get; set; }
        [MaxLength(50)]
        public string? TenQuyenHan { get; set; }
        public virtual List<TaiKhoan>? TaiKhoans { get; set; }
    }
}
