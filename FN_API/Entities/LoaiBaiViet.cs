using System.ComponentModel.DataAnnotations;

namespace FN_API.Entities
{
    public class LoaiBaiViet
    {
        [Key]
        public int LoaiBaiVietId { get; set; }
        [MaxLength(50)]
        public string? TenLoai { get; set; }
        public virtual List<ChuDe>? ChuDe { get; set; }

    }
}
