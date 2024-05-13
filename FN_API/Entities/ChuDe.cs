using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class ChuDe
    {
        [Key]
        public int ChuDeId { get; set; }
        [MaxLength(50)]
        public string? TenChuDe { get; set; }
        public string? NoiDung { get; set; }
        public int? LoaiBaiVietId { get; set; }
        public virtual List<BaiViet>? BaiViets { get; set; }
        public virtual LoaiBaiViet? LoaiBaiViet { get; set; }

    }
}
