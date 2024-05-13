using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class TinhTrangHoc
    {
        [Key]
        public int TinhTrangHocId { get; set; }
        [MaxLength(40)]
        public string? TenTinhTrang { get; set; }
        public virtual List<DangKyHoc>? DangKyHocs { get; set; }

    }
}
