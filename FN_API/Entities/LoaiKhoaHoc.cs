using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class LoaiKhoaHoc
    {
        [Key]
        public int LoaiKhoaHocId { get; set; }
        [MaxLength(30)]
        public string? TenLoai { get; set; }
        public virtual List<KhoaHoc>? KhoaHocs { get; set; }
    }
}
